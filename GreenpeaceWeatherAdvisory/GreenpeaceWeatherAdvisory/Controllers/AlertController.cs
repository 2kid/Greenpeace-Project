using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using GreenpeaceWeatherAdvisory.Models;

namespace GreenpeaceWeatherAdvisory.Controllers
{
    [Authorize]
    public class AlertController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            List<ChikkaMessage> list = db.ChikkaMessages.ToList();
            return View(list);
        }

        // Alert
        public ActionResult Create()
        {
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RegionId,Message")] ChikkaSendRequestVM vm)
        {
            if (ModelState.IsValid)
            {
                List<ContactDetail> contactDetail = db.ContactDetails.Where(m => m.Farmer.RegionId == vm.RegionId).ToList();
                WebClient wc = new WebClient();

                foreach (ContactDetail Contact in contactDetail)
                {
                    ChikkaMessage message = new ChikkaMessage();
                    message.ContactId = Contact.ContactDetailId;
                    message.Message = vm.Message;
                    message.Status = "Pending";
                    db.ChikkaMessages.Add(message);

                    message = null;
                    db.SaveChanges();
                    message = db.ChikkaMessages.OrderByDescending(m => m.ChikkaMessageId).First();

                    try
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                        string HtmlResult = wc.UploadString(Helper.Chikka.RequestUrl, vm.ParameterString(Contact.MobileNumber, message.ChikkaMessageId));
                        message.Status = "Sent";
                        db.Entry(message).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (WebException exception)
                    {
                        string responseText;

                        if (exception.Response != null)
                        {
                            var responseStream = exception.Response.GetResponseStream();

                            if (responseStream != null)
                            {
                                using (var reader = new StreamReader(responseStream))
                                {
                                    responseText = reader.ReadToEnd();
                                    
                                }
                            }
                        }
                    }

                    //

                }
                //db.ChikkaMessages.Add();
                await db.SaveChangesAsync();
                //return RedirectToAction("Result");
                return RedirectToAction("Index", "Home");
            }

            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name");
            return View(vm);
        }
    }
}