using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BookingEvents.Models;

namespace BookingEvents.Models
{
    public class FoodOrder
    {
        [Key]
        public string cart_item_id { get; set; }
        public string cart_id { get; set; }
        public int item_id { get; set; }
        [Display(Name = "Quantity")]

        public int quantity { get; set; }
        [DataType(DataType.Currency),Display(Name ="Price")]
        public double price { get; set; }
        [Display(Name ="Meal Name")]
        public string ItemName { get; set; }
        [Display(Name = "User Email")]

        public string UserEmail { get; set; }

        [Display(Name = "Picture")]
        //[DataType(DataType.Upload)]
        public byte[] Picture { get; set; }
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
        [Display(Name ="Order Status")]
        public string OrderStatus { get; set; }
        [Display(Name ="Order Date")]
        public string OrderDate { get; set; }
        [Display(Name ="Day Of Week")]
        public string DayOfWik { get; set; }

        public int? OrderId { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();
        
    }
    public class MealOrder
    {
        [Key]
        [Display(Name = "Order Number")]

        public int OrderId { get; set; }
        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; }
        [Display(Name = "User Email")]

        public string UserOrder { get; set; }
        [DataType(DataType.Currency)]
        public double Total { get; set; }
        public string Status { get; set; }
        [Display(Name = "Order Date"), DataType(DataType.Date)]

        public string OrderDate { get; set; }
        public string GenVoucher()
        {

            Guid g = Guid.NewGuid();
            Random rn = new Random();
            string gs = g.ToString();
            int randomInt = rn.Next(10, 15 + 1);
            string voucher = gs.Substring(gs.Length - randomInt - 1, randomInt);
            return voucher;

        }
    }
}