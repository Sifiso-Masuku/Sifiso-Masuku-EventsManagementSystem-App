using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingEvents.Models.ShoppingCart
{
    public class CartModel
    {
        public int Category_ID { get; set; }
        public string Name { get; set; }
        public int Department_ID { get; set; }
    }
}