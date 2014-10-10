using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chikka_Test.Models;
using System.IO;

namespace Chikka_Test.Controllers
{
    public class AlertController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // Alert
        public ActionResult Index()
        {
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "RegionId,Message")] ChikkaSendRequestVM vm)
        {
            if (ModelState.IsValid)
            {
                List<ContactDetail> contactDetail = db.ContactDetails.Where(m => m.Farmer.RegionId == vm.RegionId).ToList();
                WebClient wc = new WebClient();

                foreach (ContactDetail Contact in contactDetail)
                {
                    ChikkaMessage message = new ChikkaMessage();
                    bool failedSms = false;
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

                        message.Status = "Success";
                        db.Entry(message).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                    catch (WebException e)
                    {
                        failedSms = true;
                        string exceptionMessage = e.Message;
                        //string responseText;

                        //if (exception.Response != null)
                        //{
                        //    var responseStream = exception.Response.GetResponseStream();

                        //    if (responseStream != null)
                        //    {
                        //        using (var reader = new StreamReader(responseStream))
                        //        {
                        //            responseText = reader.ReadToEnd();

                        //        }
                        //    }
                        //}
                    }

                    // Failed

                    if (failedSms)
                    {
                        db.Entry(message).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }

                }
                
                return RedirectToAction("Index", "Home");
            }

            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name");
            return View(vm);
        }
    }
}