using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingEvents.Model;
using BookingEvents.Models;

namespace BookingEvents.Controllers
{
    public class EventsController : Controller
    {
        private EventLogic logic;
        ApplicationDbContext db = new ApplicationDbContext();
        public EventsController()
        {
            this.logic = new EventLogic();
        }

        // GET: Events
        public ActionResult Index()
        {
            return View(logic.GetEvent_s());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Event_Type event_Type)
        {
            var ve = db.Events.Where(p => p.EventName == event_Type.EventName).Count();
            if (ve != 0)
            {
                TempData["AlertMessage"] = "Cannot add " + event_Type.EventName + " because it already exists";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    logic.Add(event_Type);

                    return RedirectToAction("Index");
                }
            }
            
            return View(event_Type);
        }
    }
}