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
    [Authorize]
    public class MobileNumbersController : Controller
    {
        private DBContext db = new DBContext();

        // GET: MobileNumbers
        public ActionResult Index()
        {
            return View(db.MobileNumbers.ToList());
        }

        // GET: MobileNumbers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MobileNumber mobileNumber = db.MobileNumbers.Find(id);
            if (mobileNumber == null)
            {
                return HttpNotFound();
            }
            return View(mobileNumber);
        }

        // GET: MobileNumbers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MobileNumbers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MobileNumberID,MobileNo")] MobileNumber mobileNumber)
        {
            if (ModelState.IsValid)
            {
                db.MobileNumbers.Add(mobileNumber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mobileNumber);
        }

        // GET: MobileNumbers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MobileNumber mobileNumber = db.MobileNumbers.Find(id);
            if (mobileNumber == null)
            {
                return HttpNotFound();
            }
            return View(mobileNumber);
        }

        // POST: MobileNumbers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MobileNumberID,MobileNo")] MobileNumber mobileNumber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mobileNumber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mobileNumber);
        }

        // GET: MobileNumbers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MobileNumber mobileNumber = db.MobileNumbers.Find(id);
            if (mobileNumber == null)
            {
                return HttpNotFound();
            }
            return View(mobileNumber);
        }

        // POST: MobileNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MobileNumber mobileNumber = db.MobileNumbers.Find(id);
            db.MobileNumbers.Remove(mobileNumber);
            db.SaveChanges();
            return RedirectToAction("Index");
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
