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
    public class Booking2Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Booking2
        public ActionResult Index()
        {
            var booking2 = db.booking2.Include(b => b.Event_Type);
            return View(booking2.ToList());
        }
      
        // GET: Booking2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking2 booking2 = db.booking2.Find(id);
            if (booking2 == null)
            {
                return HttpNotFound();
            }
            return View(booking2);
        }

        // GET: Booking2/Create
        public ActionResult Create(int id)
        {
            ViewBag.Id = id;
            // ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name");
            ViewBag.CustId = new SelectList(db.Customers, "CustId", "Name");
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventName");
            ViewBag.venueId = new SelectList(db.Venue, "venueId", "venueName");
            return View();
        }

        // POST: Booking2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,DateCreated,StartDate,duration,NumPeople,EventCost,CustId,EventId,venueId,EndDate")] Booking2 booking2)
        {
            var username = User.Identity.GetUserName();

            var d = db.booking2.ToList().Where(p => p.StartDate == booking2.StartDate).Count();
            var dt = db.booking2.ToList().Where(p => p.EndDate == booking2.EndDate).Count();
            var dta = db.booking2.ToList().Where(p => p.EndDate == booking2.EndDate).Select(p=>p.EndDate).FirstOrDefault();
            ViewBag.Id = booking2.venueId;
            if (d!=0 && dt != 0)
            {
                TempData["AlertMessage"] = "Please book for other dates, Please book for a date later than - " + dta;
            }
            else
            {
                if (ModelState.IsValid)
                {
                   
                    FoodOrder b = new FoodOrder();
                    b.UserEmail = username;
                    booking2.EventCost = booking2.CalcAmount();
                    booking2.DateCreated = DateTime.Now;
                    booking2.Venuu = booking2.GetVenueNAmn();
                    db.booking2.Add(booking2);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Shopping");
                }
            }
            

           // ViewBag.CustId = new SelectList(db.Customers, "CustId", "Name", booking2.CustId);
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventName", booking2.EventId);
            ViewBag.venueId = new SelectList(db.Venue, "venueId", "venueName", booking2.venueId);
            return View(booking2);
        }
        
        






       

        // GET: Booking2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking2 booking2 = db.booking2.Find(id);
            if (booking2 == null)
            {
                return HttpNotFound();
            }
            
           // ViewBag.CustId = new SelectList(db.Customers, "CustId", "Name", booking2.CustId);
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventName", booking2.EventId);
            ViewBag.venueId = new SelectList(db.Venue, "venueId", "venueName", booking2.venueId);
            return View(booking2);
        }

        // POST: Booking2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,DateCreated,EventDate,duration,NumPeople,EventCost,CustId,EventId,venueId,ArtistId,FoodId,FlowerId,LightId")] Booking2 booking2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
           // ViewBag.CustId = new SelectList(db.Customers, "CustId", "Name", booking2.CustId);
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventName", booking2.EventId);
            ViewBag.venueId = new SelectList(db.Venue, "venueId", "venueName", booking2.venueId);
            return View(booking2);
        }

        // GET: Booking2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking2 booking2 = db.booking2.Find(id);
            if (booking2 == null)
            {
                return HttpNotFound();
            }
            return View(booking2);
        }

        // POST: Booking2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking2 booking2 = db.booking2.Find(id);
            db.booking2.Remove(booking2);
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




