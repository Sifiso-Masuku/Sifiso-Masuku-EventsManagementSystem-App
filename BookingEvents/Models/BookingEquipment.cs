using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;


namespace BookingEvents.Models
{
    public class BookingEquipment
    {
        [Required, DisplayName("Booking Equipment Id")]
        public int BookingEquipId { get; set; }
        [Required, DisplayName(" Equipment Name")]
        public double Name { get; set; }
        [Required, DisplayName("Equipment Id")]
        public int EquipmentId { get; set; }
        [Required, DisplayName("Created Date")]
       
        public DateTime Createdate { get; set; }




    }
}