using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookingEvents.Models
{
    public class Staff
    {
        [Key]
        public int StaffID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
       // public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}