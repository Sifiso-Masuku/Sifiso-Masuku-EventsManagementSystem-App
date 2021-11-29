using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookingEvents.Models;

namespace BookingEvents.Controllers
{
    public class LightsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Lights
        public ActionResult Index()
        {
            return View(db.Lights.ToList());
        }

        // GET: Lights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddLights lights = db.Lights.Find(id);
            if (lights == null)
            {
                return HttpNotFound();
            }
            return View(lights);
        }

        // GET: Lights/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LightId,Name,Cost")] AddLights lights)
        {
            if (ModelState.IsValid)
            {
                db.Lights.Add(lights);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lights);
        }

        // GET: Lights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddLights lights = db.Lights.Find(id);
            if (lights == null)
            {
                return HttpNotFound();
            }
            return View(lights);
        }

        // POST: Lights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LightId,Name,Cost")] AddLights lights)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lights).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lights);
        }

        // GET: Lights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddLights lights = db.Lights.Find(id);
            if (lights == null)
            {
                return HttpNotFound();
            }
            return View(lights);
        }

        // POST: Lights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AddLights lights = db.Lights.Find(id);
            db.Lights.Remove(lights);
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
