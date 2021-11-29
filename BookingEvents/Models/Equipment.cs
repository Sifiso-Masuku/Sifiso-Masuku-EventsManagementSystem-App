using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookingEvents.Models
{
    public class Equipment
    {
        [Key]
        public int eqpId { get; set; }
        public bool Dj { get; set; }
        public bool SoundSystem { get; set; }
        public bool stage { get; set; }
    }
}