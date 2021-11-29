 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace BookingEvents.Models
{
    public class Event_Type
    {
     [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }
        [Required, DisplayName(" Event Type")]
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        [Required, DisplayName("Basic Price")]
        [DataType(DataType.Currency)]
        public double BasicPrice { get; set; }

    }
}