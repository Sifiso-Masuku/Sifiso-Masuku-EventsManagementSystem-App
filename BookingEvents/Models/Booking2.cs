using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace BookingEvents.Models
{
    public class Booking2 : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        [Display(Name = "Customer Email")]
        public string creator { get; set; }

        [Display(Name ="Date Created"),DataType(DataType.Date)]

        public DateTime DateCreated { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        [Required, DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        [Range(100, 100000, ErrorMessage = "Number of People must be Greater than 100")]
        public int NumPeople { get; set; }

        [DisplayName("Event Name")]
        public int EventId { get; set; }
        public virtual Event_Type Event_Type { get; set; }

        [DataType(DataType.Currency)]
        public double EventCost { get; set; }

        [DisplayName("Venue Name")]
        public int venueId { get; set; }

        [DisplayName("Total Amount")]
        public decimal TotalCost { get; set; }

        [DisplayName("Status")]
        public string approval { get; set; }
        public string Venuu { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult("EndDate must be greater than StartDate");
            }


            if (StartDate < DateTime.Now)
            {
                yield return new ValidationResult("Start Date must be greater than Current Date");
            }
        }
   

        ApplicationDbContext db = new ApplicationDbContext();

        public double GetPrice()
        {
            var p = (from e in db.Events
                     where e.EventId == EventId
                     select e.BasicPrice).FirstOrDefault();
            return p;
        }
        public double CalcTotalPrice()
        {
            return GetPrice() + EventCost;
        }

        public double GetVenueP()
        {
            var rr = (from r in db.Venue
                      where r.venueId == venueId
                      select r.Cost).FirstOrDefault();
            return rr;
        }
        public string GetVenueNAmn()
        {
            var rr = (from r in db.Venue
                      where r.venueId == venueId
                      select r.venueName).FirstOrDefault();
            return rr;
        }
        //public double GetArtistCost()
        //{
        //    var p = (from e in db.AddArtists
        //             where e.ArtistId == ArtistId
        //             select e.Cost).FirstOrDefault();
        //    return p;
        //}
        //public double GetFoodCost()
        //{
        //    var p = (from e in db.AddFoods
        //             where e.FoodId == FoodId
        //             select e.Cost).FirstOrDefault();
        //    return p;
        //}



        public double CalcAmount()
        {
            return (GetPrice()) + GetVenueP();
        }
    
        //public double getVenueCost(int Venue)
        //{

        //    var u = db.Venue.Find(Venue);
        //    return Convert.ToDouble(u.Cost);
        //}

      
       
    }

    



}

