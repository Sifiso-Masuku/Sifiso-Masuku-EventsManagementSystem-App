using BookingEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookingEvents.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Gallery()
        {
            ViewBag.Message = "";

            return View();
        }
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult EquipmentInBooking(string id)
        {


            var ItemOrder = db.FoodOrders.ToList();
            //if (!String.IsNullOrEmpty(id))
            //{
            //return View(ItemOrder.Where(x => x.OrderId == id));
            //}
            //else
            //{
            return View(ItemOrder.ToList());
            //}


        }

    }
}