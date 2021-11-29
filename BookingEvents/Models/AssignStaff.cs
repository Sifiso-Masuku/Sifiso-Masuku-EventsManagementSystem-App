using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookingEvents.Models
{
    public class AssignStaff
    {
        [Key]
        public int assignID { get; set; }

        public int StaffID { get; set; }
        public virtual Staff staff { get; set; }
        public int OrderId { get; set; }
        public string customer { get; set; }
        public string Venue { get; set; }
        public string Event { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();
        public string user()
        {
            var u = (from s in db.orders
                     where OrderId == s.OrderId
                     select s.creator).FirstOrDefault();
            return u;
        }
        public string Events()
        {
            var u = (from s in db.orders
                     where OrderId == s.OrderId
                     select s.Eventt).FirstOrDefault();
            return u;
        }
        public string Venues()
        {
            var u = (from s in db.orders
                     where OrderId == s.OrderId
                     select s.Venuu).FirstOrDefault();
            return u;
        }
    }
}