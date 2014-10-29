using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GreenpeaceWeatherAdvisory.Models;
using GreenpeaceWeatherAdvisory;

namespace GreenpeaceWeatherAdvisory.Controllers
{
    public class AdvisoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Advisories
        public async Task<ActionResult> Index()
        {
            return View(await db.Advisory.ToListAsync());
        }

        // GET: Advisories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advisory advisory = await db.Advisory.FindAsync(id);
            if (advisory == null)
            {
                return HttpNotFound();
            }

            ViewBag.Advisory = advisory.Message;
            List<Recipient> RecipientList = db.Recipients.Where(m => m.AdvisoryId == id).ToList();
            List<ContactDetail> contactList = db.ContactDetails.ToList();
            List<RecipientViewModel> list = new List<RecipientViewModel>();

            foreach (var item in RecipientList)
            {
                RecipientViewModel r = new RecipientViewModel();
                r.Status = item.Status;
                r.ContactNumber = contactList.Find(m => m.ContactDetailId == item.ContactId).MobileNumber;
                list.Add(r);
            }
            
            return View(list);
        }

        // GET: Advisories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Advisories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AdvisoryId,Message")] Advisory advisory)
        {
            advisory.TimeStamp = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Advisory.Add(advisory);
                await db.SaveChangesAsync();
                advisory = db.Advisory.OrderByDescending(m => m.AdvisoryId).First();

                Helper.SendRequestFactory send = new Helper.SendRequestFactory(advisory.Message);
                List<ContactDetail> contactList = db.ContactDetails.ToList();
                foreach (var item in contactList)
                {
                    Recipient r = new Recipient();
                    r.AdvisoryId = advisory.AdvisoryId;
                    r.ContactId = item.ContactDetailId;
                    db.Recipients.Add(r);
                    db.SaveChanges();
                    r = db.Recipients.OrderByDescending(m => m.RecipientId).First();

                    using (WebClient wc = new WebClient())
                    {
                        try
                        {
                            
                            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                            wc.UploadString(Helper.Constants.REQUEST_URL, send.ParameterString(item.MobileNumber, r.RecipientId));
                            r.Status = "Sent";
                            
                        }
                        catch (WebException)
                        {
                            r.Status = "Failed";
                        }
                        finally
                        {
                            db.Entry(r).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }

                return RedirectToAction("Index");
            }

            return View(advisory);
        }   


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
