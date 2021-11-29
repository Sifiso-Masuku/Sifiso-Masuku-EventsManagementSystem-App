//using OrderMgmtUsingEF.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using BookingEvents.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingEvents.ViewModel
{
    public class CustomerOrderProduct
    {
        [Key]
        public int CustomerOrderProductID { get; set; }
        [DisplayName("Customer Email")]
        public string CustomerEmail { get; set; }
        [DisplayName("Order No")]
        public int OrderID { get; set; }
        [DisplayName("Name")]
        public string  CustomerName { get; set; }
        [DisplayName("Status")]
        public string OrderStatus { get; set; }
        public int NumberOfPeople { get; set; }
        public string Venue { get; set; }
        public int Event { get; set; }
        public double VenuePrice { get; set; }
        public double EventPrice { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        [Required, DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }
        public OrderProduct order { get; set; }
        public List<OrderProduct> orderProducts { get; set; }
        [DataType(DataType.Currency)]

        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }
        public Order or { get; set; }
        public List<Order> orders { get; set; }
    }
    public class OrderProduct
    {
        
        [DisplayName("Item Code")]
        public int ProductID { get; set; }
        [DisplayName("Item Name")]
        public string ProductName { get; set; }
       
        [DisplayName("Price")]
        public double UnitPrice { get; set; }
        [DisplayName("Quantity")]
        public int dty { get; set; }
        [DisplayName("Sub-Total")]
        public double TotalPrice { get; set; }
    }

}