using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookingEvents.Models;
using BookingEvents.ViewModel;
using Rotativa;

namespace BookingEvents.Controllers
{    
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var kl = db.orders.ToList();
                if (User.IsInRole("Customer"))
            {
                kl = kl.Where(o => o.creator == User.Identity.Name).ToList();
            }
                return View(kl);
        }

        public ActionResult Index2()
        {
            var kl = db.orders.ToList();
            return View(kl);
        }
        public ActionResult PrintInvoice(int id, string st)
        {
            var report = new ActionAsPdf("CreateInvoice", new { id = id, st = st });
            return report;
        }
        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create(int id,double c)
        {
           
            ViewBag.Id = id;
            // ViewBag.Id = id;
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventName");
            //ViewBag.venueId = new SelectList(db.Venue, "venueId", "venueName");
            Order order = new Order();
            order.venueId = id;
            order.TotalCost = (decimal)c;
          
            return View(order);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            var d = db.orders.ToList().Where(p => p.StartDate == order.StartDate).Count();
            var dt = db.orders.ToList().Where(p => p.EndDate == order.EndDate).Count();
            var dta = db.orders.ToList().Where(p => p.EndDate == order.EndDate).Select(p => p.EndDate).FirstOrDefault();
            ViewBag.venueId = order.venueId;
            var venuecap = db.Venue.ToList().Where(p => p.venueId == ViewBag.venueId).Select(x => x.NumGests).FirstOrDefault();
         
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventName");



            if (d != 0 && dt != 0)
            {
                TempData["AlertMessage"] = "Please book for other dates, Please book for a date later than - " + dta;
            }

            else if (order.NumPeople > venuecap)
            {
                TempData["AlertMessage"] = "A number of guest should not be greater than the capacity of the venue which is " + venuecap;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    order.Eventt = order.GetEvent();
                    order.TotalCost = Convert.ToDecimal(order.CalcAmount());
                    order.venue = order.GetVenueP();
                    order.creator = User.Identity.Name;
                    order.EventCost = order.GetPrice();
                    order.DateCreated = DateTime.Now;
                    order.Venuu = order.GetVenueNAmn();
                    
                    // order.TotalCost = ViewBag.Total;
                    db.orders.Add(order);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Shopping");
                }
               
            }        
            return View(order);
        }
        public ActionResult ConfirmBooking(int id)
        {
            var d = db.orders.Where(p => p.OrderId == id).FirstOrDefault();
            if (d.approval == "Rejected")
            {
                TempData["AlertMessage"] = "Cannot assign staff to a booking that has been rejected";
                return RedirectToAction("Index2");

            }
            else if (d.approval == "Approved")
            {
                TempData["AlertMessage"] = "Cannot assign a booking with staff twice";
                return RedirectToAction("Index2");

            }
            else
            {
                return RedirectToAction("Create", "AssignStaffs", new { id = id });

            }

            // return RedirectToAction("Index", db.Bookings.ToList());
        }
        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,OrderDate,TotalCost,location,approval,creator")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }
        public ActionResult Approve(int Id)
        {
            var order = db.orders.Where(ui => ui.OrderId == Id).FirstOrDefault();
            if(order.approval == "Rejected")
            {
                TempData["AlertMessage"] = "Cannot Approve an order that has been Rejected Already";
                return RedirectToAction("Index2");

            }
            else
            if (order.approval == "Approved")
            {
                TempData["AlertMessage"] = "Order Already Approved";
                return RedirectToAction("Index2");
            }
            else
            {
                order.approval = "Approved";
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create","AssignStaffs", new { id=order.OrderId});
            }
        }
        public ActionResult Reject(int Id)
        {
            var order = db.orders.Where(ui => ui.OrderId == Id).FirstOrDefault();
            if (order.approval == "Approved")
            {
                TempData["AlertMessage"] = "Cannot reject an order that has been Approved Already";
                return RedirectToAction("Index2");

            }
            else
            if (order.approval == "Rejected")
            {
                TempData["AlertMessage"] = "Order Already Rejected";
                return RedirectToAction("Index2");
            }
            else
            {
                order.approval = "Rejected";
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index2");
            }

        }

        public Order ordersd(int? id)
        {
            var df = db.orders.Where(k => k.OrderId == id).FirstOrDefault();
            return df;
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        public ActionResult CreateInvoice(int id,string st)
        {
            var allConfirmedOrders = db.FoodOrders.Where(o => o.OrderId == id).ToList();
            CustomerOrderProduct allRecords = new CustomerOrderProduct();
            List<OrderProduct> allRe = new List<OrderProduct>();
      
            foreach (var item in allConfirmedOrders)
            {
                OrderProduct kl = new OrderProduct()
                {
                    ProductID = item.item_id,
                    ProductName = item.ItemName,
                    UnitPrice = item.price,
                    TotalPrice = (item.price * item.quantity),
                    dty=item.quantity
                   
                    ///OrderStatus = item.OrderStatus,

                };
                allRe.Add(kl);
                CustomerOrderProduct obj2 = new CustomerOrderProduct()
                {
                    CustomerEmail = item.UserEmail,
                    OrderID = (int)item.OrderId,
                    OrderStatus = st,
                    Venue = ordersd(item.OrderId).Venuu,
                    NumberOfPeople= ordersd(item.OrderId).NumPeople,
                    Event= ordersd(item.OrderId).EventId,
                    EventPrice= ordersd(item.OrderId).EventCost,
                    VenuePrice= ordersd(item.OrderId).GetVenueP(),
                    StartDate= ordersd(item.OrderId).StartDate,
                    EndDate= ordersd(item.OrderId).EndDate,
                    TotalAmount= ordersd(item.OrderId).TotalCost,
                    CustomerName = db.Users.Where(o=>o.Email==item.UserEmail).Select(p=>p.Name).FirstOrDefault()
                };
                allRecords = obj2;
            }

            allRecords.orderProducts = allRe;

            ViewBag.invoiceNumber = DateTime.Now.ToString("yyyyMMddHHmmss");
            return View(allRecords);
        }
        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.orders.Find(id);
            db.orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index2");
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
