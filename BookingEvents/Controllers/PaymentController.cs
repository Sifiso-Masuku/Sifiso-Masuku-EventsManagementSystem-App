using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayFast;
using PayFast.AspNet;
using System.Configuration;
using BookingEvents.Models;

namespace BookingEvents.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PayFastSettings payFastSettings;
        public PaymentController()
        {
            this.payFastSettings = new PayFastSettings();
            this.payFastSettings.MerchantId = ConfigurationManager.AppSettings["MerchantId"];
            this.payFastSettings.MerchantKey = ConfigurationManager.AppSettings["MerchantKey"];
            this.payFastSettings.PassPhrase = ConfigurationManager.AppSettings["PassPhrase"];
            this.payFastSettings.ProcessUrl = ConfigurationManager.AppSettings["ProcessUrl"];
            this.payFastSettings.ValidateUrl = ConfigurationManager.AppSettings["ValidateUrl"];
            this.payFastSettings.ReturnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            this.payFastSettings.CancelUrl = ConfigurationManager.AppSettings["CancelUrl"];
            this.payFastSettings.NotifyUrl = ConfigurationManager.AppSettings["NotifyUrl"];
        }
        // GET: Payment
        public ActionResult OnceOff(double payment, int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var ordr = db.orders.Where(p => p.OrderId == id).Select(p => p.isPaid).FirstOrDefault();
            ordr = true;

            var onceOffRequest = new PayFastRequest(this.payFastSettings.PassPhrase);
                //var callbackUrl = Url.Action("Create", "Parents",null, protocol: Request.Url.Scheme);
                var callbackUrl = Url.Action("Return", "Payment", payment);

                // Merchant Details
                onceOffRequest.merchant_id = this.payFastSettings.MerchantId;
                onceOffRequest.merchant_key = this.payFastSettings.MerchantKey;
                onceOffRequest.SetPassPhrase(this.payFastSettings.PassPhrase);
                //onceOffRequest.return_url = this.payFastSettings.ReturnUrl;
                onceOffRequest.return_url = this.payFastSettings.ReturnUrl;

                onceOffRequest.cancel_url = this.payFastSettings.CancelUrl;
                //onceOffRequest.notify_url = this.payFastSettings.NotifyUrl;

                // Buyer Details
                onceOffRequest.email_address = "sbtu01@payfast.co.za";

                // Transaction Details 4e8d1379-688c-4d12-8d88-e9ec6078358f
                onceOffRequest.m_payment_id = "8d00bf49-e979-4004-228c-08d452b86380";
                onceOffRequest.amount = payment;
                onceOffRequest.item_name = "Once off option";
                onceOffRequest.item_description = "Some details about the once off payment";

                // Transaction Options
                onceOffRequest.email_confirmation = true;
                onceOffRequest.confirmation_address = "suphiwok@gmail.com";
            //invoice 
            
                var redirectUrl = $"{this.payFastSettings.ProcessUrl}{onceOffRequest.ToString()}";
                // fb.createPayment(payment);
                //fb.updateFee(payment.feeId, payment.amountPayed);
                return Redirect(redirectUrl);

        }

        // GET: Payment/Details/5
        public ActionResult Return()
        {
            return View();
        }

        // GET: Payment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payment/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Payment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Payment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Payment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Payment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
