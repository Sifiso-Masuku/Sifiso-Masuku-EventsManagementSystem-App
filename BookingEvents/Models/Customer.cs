using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookingEvents.Models
{
    public class Customer
    {
        [Key]

        public string CustId { get; set; }
 
        public string Name { get; set; }
 
        public string Surname { get; set; }
     
        public string Email { get; set; }

        public string Address { get; set; }
    
        public string PhoneNum { get; set; }
    }
}