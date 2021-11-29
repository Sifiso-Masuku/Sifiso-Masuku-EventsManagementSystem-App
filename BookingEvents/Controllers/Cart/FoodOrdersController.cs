using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Helpers;
using System.Collections;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;
using System.Data.Entity;
using BookingEvents.Models;

namespace BookingEvents.Controllers
{
    public class FoodOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Cart_Service cart_Service = new Cart_Service();
        // GET: FoodOrders
        public ActionResult Index()
        {
            var UserName = User.Identity.GetUserName();
            return View(db.FoodOrders.ToList().Where(x=>x.UserEmail==UserName && x.OrderStatus!= "Checked Out"));
        }

       

        public ActionResult COnfirmOrder()
        {
            var UserName = User.Identity.GetUserName();
            ViewBag.Total = cart_Service.GetCartTotal(cart_Service.GetCartID());
            ViewBag.TotalQTY = cart_Service.GetCartItems().FindAll(x => x.cart_id == cart_Service.GetCartID()).Sum(q => q.quantity);
            var confirm = db.FoodOrders.ToList();
            var cart = db.Cart_Items.ToList();

            MealOrder mealOrder = new MealOrder();
            mealOrder.OrderNumber = mealOrder.GenVoucher();
            mealOrder.Total = ViewBag.Total;
            mealOrder.UserOrder = UserName;
            mealOrder.Status = "Paid";
            mealOrder.OrderDate = DateTime.Now.Date.ToLongDateString();
            db.MealOrders.Add(mealOrder);
            db.SaveChanges();

            FoodOrder foodOrder = new FoodOrder();

            foreach (var item in confirm)
            {
                foreach (var i in cart)
                {
                    if (UserName == item.UserEmail && item.cart_id == i.cart_id)
                    {
                        var statusUpdate = db.FoodOrders.Find(item.cart_item_id);
                        statusUpdate.OrderStatus = "Checked Out";
                        statusUpdate.OrderId = mealOrder.OrderId;
                        db.Entry(statusUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                        cart_Service.EmptyCart();
                        

                    }
                }

            }

            return RedirectToAction("OnceOff",new { tot=mealOrder.Total});
        }

        //private readonly PayFastSettings payFastSettings;


        //#region Constructor

        //public FoodOrdersController()
        //{
        //    this.payFastSettings = new PayFastSettings();
        //    this.payFastSettings.MerchantId = ConfigurationManager.AppSettings["MerchantId"];
        //    this.payFastSettings.MerchantKey = ConfigurationManager.AppSettings["MerchantKey"];
        //    this.payFastSettings.PassPhrase = ConfigurationManager.AppSettings["PassPhrase"];
        //    this.payFastSettings.ProcessUrl = ConfigurationManager.AppSettings["ProcessUrl"];
        //    this.payFastSettings.ValidateUrl = ConfigurationManager.AppSettings["ValidateUrl"];
        //    this.payFastSettings.ReturnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
        //    this.payFastSettings.CancelUrl = ConfigurationManager.AppSettings["CancelUrl"];
        //    this.payFastSettings.NotifyUrl = ConfigurationManager.AppSettings["NotifyUrl"];
        //}

        //#endregion Constructor

        //#region Methods



        //public ActionResult Recurring()
        //{

        //    var recurringRequest = new PayFastRequest(this.payFastSettings.PassPhrase);

        //    // Merchant Details
        //    recurringRequest.merchant_id = this.payFastSettings.MerchantId;
        //    recurringRequest.merchant_key = this.payFastSettings.MerchantKey;
        //    recurringRequest.return_url = this.payFastSettings.ReturnUrl;
        //    recurringRequest.cancel_url = this.payFastSettings.CancelUrl;
        //    recurringRequest.notify_url = this.payFastSettings.NotifyUrl;

        //    // Buyer Details
        //    recurringRequest.email_address = "sbtu01@payfast.co.za";

        //    // Transaction Details
        //    recurringRequest.m_payment_id = "8d00bf49-e979-4004-228c-08d452b86380";
        //    recurringRequest.amount = 20;
        //    recurringRequest.item_name = "Recurring Option";
        //    recurringRequest.item_description = "Some details about the recurring option";

        //    // Transaction Options
        //    recurringRequest.email_confirmation = true;
        //    recurringRequest.confirmation_address = "drnendwandwe@gmail.com";

        //    // Recurring Billing Details
        //    recurringRequest.subscription_type = SubscriptionType.Subscription;
        //    recurringRequest.billing_date = DateTime.Now;
        //    recurringRequest.recurring_amount = 20;
        //    recurringRequest.frequency = BillingFrequency.Monthly;
        //    recurringRequest.cycles = 0;

        //    var redirectUrl = $"{this.payFastSettings.ProcessUrl}{recurringRequest.ToString()}";

        //    return Redirect(redirectUrl);
        //}




        //public ActionResult ListsOrders()
        //{
        //    var username = User.Identity.GetUserName();
        //    if (User.IsInRole("Student"))
        //    {
        //        return View(db.MealOrders.ToList().Where(x=>x.UserOrder==username));

        //    }
        //    else
        //    {
        //        return View(db.MealOrders.ToList());

        //    }
        //}



        //public ActionResult OnceOff(double tot)
        //{
        //    var onceOffRequest = new PayFastRequest(this.payFastSettings.PassPhrase);

        //    // Merchant Details
        //    onceOffRequest.merchant_id = this.payFastSettings.MerchantId;
        //    onceOffRequest.merchant_key = this.payFastSettings.MerchantKey;
        //    onceOffRequest.return_url = this.payFastSettings.ReturnUrl;
        //    onceOffRequest.cancel_url = this.payFastSettings.CancelUrl;
        //    onceOffRequest.notify_url = this.payFastSettings.NotifyUrl;

        //    // Buyer Details
        //    onceOffRequest.email_address = "sbtu01@payfast.co.za";
        //    double amount = Convert.ToDouble(db.Items.Select(x => x.CostPrice).FirstOrDefault());
        //    var products = db.Items.Select(x => x.Name).ToList();
        //    // Transaction Details
        //    onceOffRequest.m_payment_id = "";
        //    onceOffRequest.amount = tot;
        //    onceOffRequest.item_name = "Once off option";
        //    onceOffRequest.item_description = "Some details about the once off payment";


        //    // Transaction Options
        //    onceOffRequest.email_confirmation = true;
        //    onceOffRequest.confirmation_address = "sbtu01@payfast.co.za";

        //    var redirectUrl = $"{this.payFastSettings.ProcessUrl}{onceOffRequest.ToString()}";

        //    return Redirect(redirectUrl);
        //}

        //public ActionResult AdHoc()
        //{
        //    var adHocRequest = new PayFastRequest(this.payFastSettings.PassPhrase);

        //    // Merchant Details
        //    adHocRequest.merchant_id = this.payFastSettings.MerchantId;
        //    adHocRequest.merchant_key = this.payFastSettings.MerchantKey;
        //    adHocRequest.return_url = this.payFastSettings.ReturnUrl;
        //    adHocRequest.cancel_url = this.payFastSettings.CancelUrl;
        //    adHocRequest.notify_url = this.payFastSettings.NotifyUrl;
        //    #endregion Methods
        //    // Buyer Details
        //    adHocRequest.email_address = "sbtu01@payfast.co.za";
        //    double amount = Convert.ToDouble(db.FoodOrders.Select(x => x.Total).FirstOrDefault());
        //    var products = db.FoodOrders.Select(x => x.UserEmail).ToList();
        //    // Transaction Details
        //    adHocRequest.m_payment_id = "";
        //    adHocRequest.amount = 70;
        //    adHocRequest.item_name = "Adhoc Agreement";
        //    adHocRequest.item_description = "Some details about the adhoc agreement";

        //    // Transaction Options
        //    adHocRequest.email_confirmation = true;
        //    adHocRequest.confirmation_address = "sbtu01@payfast.co.za";

        //    // Recurring Billing Details
        //    adHocRequest.subscription_type = SubscriptionType.AdHoc;

        //    var redirectUrl = $"{this.payFastSettings.ProcessUrl}{adHocRequest.ToString()}";

        //    return Redirect(redirectUrl);
        //}

        //public ActionResult Return()
        //{
        //    return View();
        //}

        //public ActionResult Cancel()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<ActionResult> Notify([ModelBinder(typeof(PayFastNotifyModelBinder))]PayFastNotify payFastNotifyViewModel)
        //{
        //    payFastNotifyViewModel.SetPassPhrase(this.payFastSettings.PassPhrase);

        //    var calculatedSignature = payFastNotifyViewModel.GetCalculatedSignature();

        //    var isValid = payFastNotifyViewModel.signature == calculatedSignature;

        //    System.Diagnostics.Debug.WriteLine($"Signature Validation Result: {isValid}");

        //    // The PayFast Validator is still under developement
        //    // Its not recommended to rely on this for production use cases
        //    var payfastValidator = new PayFastValidator(this.payFastSettings, payFastNotifyViewModel, IPAddress.Parse(this.HttpContext.Request.UserHostAddress));

        //    var merchantIdValidationResult = payfastValidator.ValidateMerchantId();

        //    System.Diagnostics.Debug.WriteLine($"Merchant Id Validation Result: {merchantIdValidationResult}");

        //    var ipAddressValidationResult = payfastValidator.ValidateSourceIp();

        //    System.Diagnostics.Debug.WriteLine($"Ip Address Validation Result: {merchantIdValidationResult}");

        //    // Currently seems that the data validation only works for successful payments
        //    if (payFastNotifyViewModel.payment_status == PayFastStatics.CompletePaymentConfirmation)
        //    {
        //        var dataValidationResult = await payfastValidator.ValidateData();

        //        System.Diagnostics.Debug.WriteLine($"Data Validation Result: {dataValidationResult}");
        //    }

        //    if (payFastNotifyViewModel.payment_status == PayFastStatics.CancelledPaymentConfirmation)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Subscription was cancelled");
        //    }

        //    return new HttpStatusCodeResult(HttpStatusCode.OK);
        //}

        public ActionResult Error()
        {
            return View();
        }

    }
}
