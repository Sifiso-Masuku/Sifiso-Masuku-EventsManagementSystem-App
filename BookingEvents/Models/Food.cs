using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;


namespace BookingEvents.Models
{
    public class Food
    {
        [Key]

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodId { get; set; }

        [Required, DisplayName("Food Name")]
        public string FoodName { get; set; }
        
        public double Cost { get; set; }
       
    }
}