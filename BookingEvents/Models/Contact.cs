using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookingEvents.Models
{
    public class Contact
    {
        [Key]
        public int contactId { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression("^[a-zA-Z]{1,20}$", ErrorMessage = "Kindly Enter only Alphabets")]
        [DisplayName("First name")]
        public string CuName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [DisplayName("Last name")]
        [RegularExpression("^[a-zA-Z]{1,20}$", ErrorMessage = "Kindly Enter only Alphabets")]
        public string CuLName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address ")]
        public string Cuaddress { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d", ApplyFormatInEditMode = true)]
        public DateTime date
        {
            get { return DateTime.Now; }
        }
        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Choose Subject")]
        [DisplayName("Subject")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Write something message")]
        [DisplayName("Message")]
        public string Message { get; set; }
    }
}