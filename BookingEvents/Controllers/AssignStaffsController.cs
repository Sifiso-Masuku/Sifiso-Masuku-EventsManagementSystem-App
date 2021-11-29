using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookingEvents.Models;
using Microsoft.AspNet.Identity;

namespace BookingEvents.Controllers
{
    public class AssignStaffsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AssignStaffs
        public ActionResult Index()
        {
            var assignStaffs = db.AssignStaffs.Include(a => a.staff);
            return View(assignStaffs.ToList());
        }
        public ActionResult Index2()
        {
            var assignStaffs = db.AssignStaffs.Include(a => a.staff);
            return View(assignStaffs.ToList().Where(p=>p.customer==User.Identity.GetUserName()));
        }
        // GET: AssignStaffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignStaff assignStaff = db.AssignStaffs.Find(id);
            if (assignStaff == null)
            {
                return HttpNotFound();
            }
            return View(assignStaff);
        }

        // GET: AssignStaffs/Create
        public ActionResult Create(int id)
        {
            ViewBag.Id = id;
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "Name");
            AssignStaff assignStaff = new AssignStaff();
            assignStaff.OrderId = id;
            return View(assignStaff);
        }

        // POST: AssignStaffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "assignID,StaffID,OrderId,Venue,Event,customer")] AssignStaff assignStaff)
        {
            if (ModelState.IsValid)
            {
                var d = db.orders.Where(p => p.OrderId == assignStaff.OrderId).FirstOrDefault();
                d.approval = "Approved";
                assignStaff.customer = assignStaff.user();
                assignStaff.Venue = assignStaff.Venues();
                assignStaff.Event = assignStaff.Events();

                db.AssignStaffs.Add(assignStaff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "Name", assignStaff.StaffID);
            return View(assignStaff);
        }

        // GET: AssignStaffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignStaff assignStaff = db.AssignStaffs.Find(id);
            if (assignStaff == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "Name", assignStaff.StaffID);
            return View(assignStaff);
        }

        // POST: AssignStaffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "assignID,StaffID,OrderId")] AssignStaff assignStaff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignStaff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "Name", assignStaff.StaffID);
            return View(assignStaff);
        }

        // GET: AssignStaffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignStaff assignStaff = db.AssignStaffs.Find(id);
            if (assignStaff == null)
            {
                return HttpNotFound();
            }
            return View(assignStaff);
        }

        // POST: AssignStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignStaff assignStaff = db.AssignStaffs.Find(id);
            db.AssignStaffs.Remove(assignStaff);
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
