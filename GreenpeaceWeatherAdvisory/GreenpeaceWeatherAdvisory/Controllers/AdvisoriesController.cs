using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GreenpeaceWeatherAdvisory.Models;

namespace GreenpeaceWeatherAdvisory.Controllers
{
    [Authorize(Roles="SuperAdmin,Admin,Staff")]
    public class AdvisoriesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Advisories
        public ActionResult Index(String searchDateTime)
        {
            List<Advisory> Results = new List<Advisory>();
//            DateTime datetimeSearchDateTime = Convert.ToDateTime(searchDateTime);
           
            var advisoryList = db.Advisories.ToList();
            foreach(Advisory item in advisoryList)
            {
                if(item.DateTime.ToShortDateString() == searchDateTime)
                {
                    Results.Add(item);
                }
            }
            //var datetime = from a in db.Advisories
            //               where a.DateTime.ToShortDateString() == searchDateTime
            //               select a;

            

            return View(Results);
        }

        // GET: Advisories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advisory advisory = db.Advisories.Find(id);
            if (advisory == null)
            {
                return HttpNotFound();
            }
            return View(advisory);
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
        public ActionResult Create([Bind(Include = "AdvisoryID,Message")] Advisory advisory)
        {
            if (ModelState.IsValid)
            {
                advisory.DateTime = DateTime.Now;
                db.Advisories.Add(advisory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(advisory);
        }

        //// GET: Advisories/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Advisory advisory = db.Advisories.Find(id);
        //    if (advisory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(advisory);
        //}

        //// POST: Advisories/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "AdvisoryID,Message,DateTime")] Advisory advisory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(advisory).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(advisory);
        //}

        //// GET: Advisories/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Advisory advisory = db.Advisories.Find(id);
        //    if (advisory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(advisory);
        //}

        //// POST: Advisories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Advisory advisory = db.Advisories.Find(id);
        //    db.Advisories.Remove(advisory);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
