using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingEvents.Models
{
    public class Email
    {
        //overload with what you want to show in the email.
        public void SendConfirmation(string email,string venue, int numOfPeople, string orderdate,string eventss, double total)
        {
            try
            {
                var myMessage = new SendGridMessage
                {
                    From = new EmailAddress("no-reply@homify.co.za", "Bright Ideas")
                };

                myMessage.AddTo(email);
                string subject = "Booking: ";
                string body = (
                    "Dear " + email + "<br/>"
                    + "<br/>"
                    + "Your Booking details are as follows: "
                    + "<br/>"
                    + "<br/>" + "Booking Date: " + orderdate
                    + "<br/>" + "Venue Booked: " + venue
                    + "<br/>" + "Event Booked: " + eventss
                    + "<br/>" + "Number Of People: " + numOfPeople
                    + "<br/>" + "Total Price:" + " R" + total +
                    "<br/>" +
                    "<br/>" +
                    "<br/>" +


                    "We hope to hear from you soon." +
                    "<br/>" +
                    "Bright-Ideas");

                myMessage.Subject = subject;
                myMessage.HtmlContent = body;

                var transportWeb = new SendGrid.SendGridClient("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");

                transportWeb.SendEmailAsync(myMessage);
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }

        }


    }
}