using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BookingEvents.Models
{
    public class Venue1cs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int venueId { get; set; }
        [Required, DisplayName(" Venue Name")]
        public string venueName { get; set; }
        [Required, DisplayName(" Capacity")]
        public int NumGests { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Currency)]

        public double Cost { get; set; }

        public string ImageType { get; set; }
        public byte[] Image { get; set; }

    }
}