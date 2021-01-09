using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        // GET: Insuree
        public ActionResult Index()
        {
            return View("~/Views/Insuree/Index.cshtml", db.Insurees.ToList());
        }

        // GET: Insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                insuree.Quote = Quote(insuree);
                db.Insurees.Add(insuree);
                db.Insurees.Find(insuree.Id);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }

        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
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

        public decimal Quote(Insuree insuree)
        {

            decimal baseAmount = 50m;
            var insureeAge = DateTime.Now.Year - insuree.DateOfBirth.Year;
            
            if (insureeAge <= 18)
            {
                baseAmount += 100m;
            }
            else if (insureeAge >= 19 && insureeAge < 25)
            {
                baseAmount += 50m;
            }
            else if(insureeAge > 25)
            {
                baseAmount += 25m;
            }
            if (insuree.CarYear < 2000)
            {
                baseAmount += 25m;
            }
            else if (insuree.CarYear > 2015)
            {
                baseAmount += 25m;
            }
            if (insuree.CarMake.ToLower() == "porche")
            {
                baseAmount += 25m;
            }
            if (insuree.CarMake.ToLower() == "porche" && insuree.CarModel.ToLower() == "911 carrera")
            {
                baseAmount += 25m;
            }
            if (insuree.SpeedingTickets > 0)
            {
                baseAmount += insuree.SpeedingTickets * 10m;
            }
            if (insuree.DUI == true)
            {
                baseAmount = baseAmount * 1.25m;
            }
            if (insuree.CoverageType == true)
            {
                baseAmount = baseAmount * 1.50m;
            }

            return baseAmount;
        }

        public ActionResult Admin()
        {
            return View("Admin");
        }

    }
}
