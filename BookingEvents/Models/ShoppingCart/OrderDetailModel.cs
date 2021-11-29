using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingEvents.Models
{
    public class OrderDetailModel
    {
        public Customer customer { get; set; }
        public FoodOrder order { get; set; }
        public string shipping_method { get; set; }
        public Shipping_Address address { get; set; }
        [Display(Name ="Payment Method")]
        public string payment_Method { get; set; }
        [Display(Name = "Order Total")]
        [DataType(DataType.Currency)]
        public decimal order_total { get; set; }
    }
}
