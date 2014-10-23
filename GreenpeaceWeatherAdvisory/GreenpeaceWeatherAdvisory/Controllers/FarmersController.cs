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

namespace GreenpeaceWeatherAdvisory.Controllers
{
    [Authorize]
    public class FarmersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Farmers
        public async Task<ActionResult> Index(string SearchFarmer)
        {
            var farmers = db.Farmers.Include(f => f.Region);
           
            if(SearchFarmer!=null&& SearchFarmer!="")
            {
                farmers = db.Farmers.Where(r => r.LastName.Contains(SearchFarmer)).Include(f => f.Region);
            }
          
            return View(await farmers.ToListAsync());
        }

        // GET: Farmers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farmer farmer = await db.Farmers.FindAsync(id);
            if (farmer == null)
            {
                return HttpNotFound();
            }

            ViewBag.ContactDetails = db.ContactDetails.Where(m => m.FarmerId == id).ToList();
            return View(farmer);
        }

        // GET: Farmers/Create
        public ActionResult Create()
        {
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name");
            return View();
        }

        // POST: Farmers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FarmerId,LastName,FirstName,MiddleName,RegionId")] Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                db.Farmers.Add(farmer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name", farmer.RegionId);
            return View(farmer);
        }

        // GET: Farmers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farmer farmer = await db.Farmers.FindAsync(id);
            if (farmer == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name", farmer.RegionId);
            return View(farmer);
        }

        // POST: Farmers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FarmerId,LastName,FirstName,MiddleName,RegionId")] Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(farmer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name", farmer.RegionId);
            return View(farmer);
        }

        // GET: Farmers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farmer farmer = await db.Farmers.FindAsync(id);
            if (farmer == null)
            {
                return HttpNotFound();
            }
            return View(farmer);
        }

        // POST: Farmers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Farmer farmer = await db.Farmers.FindAsync(id);
            db.Farmers.Remove(farmer);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: ContactDetails/AddContact
        public ActionResult AddContact(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farmer farmer = db.Farmers.Find(id.Value);
            if (farmer == null)
            {
                return HttpNotFound();
            }

            ContactDetail contactDetail = new ContactDetail();
            contactDetail.FarmerId = id.Value;

            return View(contactDetail);
        }

        // POST: ContactDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddContact([Bind(Include = "MobileNumber,FarmerId")] ContactDetail contactDetail)
        {
            if (ModelState.IsValid)
            {
                db.ContactDetails.Add(contactDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = contactDetail.FarmerId });
            }

            return View(contactDetail);
        }

        // Contact Delete
        // GET: Farmers/Delete/5
        public async Task<ActionResult> ContactDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactDetail contact = await db.ContactDetails.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            
            db.ContactDetails.Remove(contact);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { id = contact.FarmerId } );
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
