using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingEvents.Models
{
    public class Order : IValidatableObject
    {
        [Key]
        [DisplayName("Order No.  ")]
        public int OrderId { get; set; }
        
       
        [DisplayName("Total Amount")]
        [DataType(DataType.Currency)]

        public decimal TotalCost { get; set; }


        [DisplayName("Order Amount")]
        public decimal FinalCost { get; set; }

        [DisplayName("Physical Address")]
        public string location { get; set; }
        [DisplayName("Status")]
        public string approval { get; set; }
        [DisplayName("Customer Email")]
        public string creator { get; set; }
        public DateTime DateCreated { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        [Required, DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        [Required, DisplayName("End Date")]

        public DateTime EndDate { get; set; }

        public int NumPeople { get; set; }

        [DisplayName("Event Name")]
        public int EventId { get; set; }
        public virtual Event_Type Event_Type { get; set; }

        [DataType(DataType.Currency)]
        public double EventCost { get; set; }
        public double venue { get; set; }

        [DisplayName("Venue Name")]
        public int venueId { get; set; }
        public int StaffID { get; set; }
        public virtual Venue1cs Venue1 { get; set; }
        public bool isPaid { get; set; }
        public string Venuu { get; set; }
        public string Eventt { get; set; }
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

        public string GetEvent()
        {
            var p = (from e in db.Events
                     where e.EventId == EventId
                     select e.EventName).FirstOrDefault();
            return p;
        }
        public double CalcTotalPrice()
        {
            return (GetPrice() + GetVenueP() + Convert.ToDouble(TotalCost));
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

        public double CalcAmount()
        {
            return (GetPrice()) + GetVenueP();
        }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}