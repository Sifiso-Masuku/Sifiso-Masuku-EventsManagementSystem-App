using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingEvents.Models;
namespace BookingEvents.Models
{
    public  class Cart_Service
    {
        private ApplicationDbContext dataContext;
        public static string shoppingCartID { get; set; }
        public const string CartSessionKey = "CartId";
        public Cart_Service()
        {
            this.dataContext = new ApplicationDbContext();
        }

        public void AddItemToCart(int id, string username)
        {
            shoppingCartID = GetCartID();
            FoodOrder food = new FoodOrder();
            var item = dataContext.Items.Find(id);
            if (item != null)
            {

                //FoodOrder
                var foodItem =
                    dataContext.FoodOrders.FirstOrDefault(x => x.cart_id == shoppingCartID && x.item_id == item.ItemCode);
                var cartItem =
                    dataContext.Cart_Items.FirstOrDefault(x => x.cart_id == shoppingCartID && x.item_id == item.ItemCode);
                if (cartItem == null)
                {
                    var cart = dataContext.Carts.Find(shoppingCartID);
                    if (cart == null)
                    {
                        dataContext.Carts.Add(entity: new Cart()
                        {
                            cart_id = shoppingCartID,
                            date_created = DateTime.Now
                            
                        });
                        dataContext.SaveChanges();
                    }

                    dataContext.Cart_Items.Add(entity: new Cart_Item()
                    {
                        cart_item_id = Guid.NewGuid().ToString(),
                        cart_id = shoppingCartID,
                        item_id = item.ItemCode,
                        quantity = 1,
                        price = item.Price,
                        UserEmail=username
                        
                    }
                        );
                    //food.item_id = cartItem.item_id;
                    //food.cart_id = cartItem.cart_id;
                    //food.quantity = cartItem.quantity;
                    //food.UserEmail = cartItem.UserEmail;
                    //dataContext.FoodOrders.Add(food);




                    dataContext.FoodOrders.Add(entity: new FoodOrder()
                    {
                        cart_item_id = Guid.NewGuid().ToString(),
                        cart_id = shoppingCartID,
                        item_id = item.ItemCode,
                        quantity = 1,
                        price = item.Price,
                        ItemName = item.Name,
                        UserEmail = username,
                        Picture = item.Picture,
                        OrderDate = DateTime.Now.Date.ToString(),
                        OrderStatus="Not Cheked Out"
                    });

                }
                else
                {
                    cartItem.quantity++;
                    foodItem.quantity++;
                }
                dataContext.SaveChanges();
            }
        }


        public void UpdateQuantity(int id, int qty)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var qtyUpdate = db.Items.Find(id);
            qtyUpdate.QuantityInStock = qtyUpdate.QuantityInStock - qty;
            db.Entry(qtyUpdate).State = EntityState.Modified;
            db.SaveChanges();
        }


        public void RemoveItemFromCart(string id)
        {
            shoppingCartID = GetCartID();

            var item = dataContext.Cart_Items.Find(id);
            if (item != null)
            {
                var cartItem =
                    dataContext.Cart_Items.FirstOrDefault(predicate: x => x.cart_id == shoppingCartID && x.item_id == item.item_id);
                var OrderItem =
                   dataContext.FoodOrders.FirstOrDefault(predicate: x => x.cart_id == shoppingCartID && x.item_id == item.item_id);
                if (cartItem != null)
                {
                    dataContext.Cart_Items.Remove(entity: cartItem);
                    dataContext.FoodOrders.Remove(entity: OrderItem);
                }
                dataContext.SaveChanges();
            }
        }
        public List<Cart_Item> GetCartItems()
        {
            shoppingCartID = GetCartID();
            return dataContext.Cart_Items.ToList().FindAll(match: x => x.cart_id == shoppingCartID);
        }
        public void UpdateCart(string id, int qty)
        {
            var item = dataContext.Cart_Items.Find(id);
            if (qty < 0)
                item.quantity = qty / -1;
            else if (qty == 0)
                RemoveItemFromCart(item.cart_item_id);
            else if (item.Item.QuantityInStock < qty)
                item.quantity = item.Item.QuantityInStock;
            else
                item.quantity = qty;
            dataContext.SaveChanges();
        }
        public double GetCartTotal(string id)
        {
            double amount = 0;
            foreach (var item in dataContext.Cart_Items.ToList().FindAll(match: x => x.cart_id == id))
            {
                amount += (item.price * item.quantity);
            }
            return amount;
        }
        public void checkout(string id)
        {
            double amount = 0;
            ApplicationDbContext db = new ApplicationDbContext();
            var orderId = db.orders.Where(p => p.creator == id).Select(p => p.OrderId).Max();
            var eve = db.orders.Where(p => p.creator == id).Select(p => p.EventCost).Max();
            var vene = db.orders.Where(p => p.creator == id).Select(p => p.venue).Max();

            foreach (var item in dataContext.FoodOrders.ToList().FindAll(match: x => x.cart_id == id && x.OrderId==null))
            {
                amount +=(item.price * item.quantity);
                item.OrderId = orderId;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            
            var or = db.orders.Where(o => o.OrderId == orderId).FirstOrDefault();
            or.TotalCost =Convert.ToDecimal(amount + eve + vene);
            or.approval = "Waiting For Approval";
            db.Entry(or).State = EntityState.Modified;
            db.SaveChanges();
            //return amount;
        }
        public void EmptyCart()
        {
            shoppingCartID = GetCartID();
            foreach (var item in dataContext.Cart_Items.ToList().FindAll(match: x => x.cart_id == shoppingCartID))
            {
                dataContext.Cart_Items.Remove(item);
            }
            try
            {
                dataContext.Carts.Remove(dataContext.Carts.Find(shoppingCartID));
                dataContext.SaveChanges();
            }
            catch (Exception ex) { }
        }
        public string GetCartID()
        {
            if (System.Web.HttpContext.Current.Session[name: CartSessionKey] == null)
            {
                if (!String.IsNullOrWhiteSpace(value: System.Web.HttpContext.Current.User.Identity.Name))
                {
                    System.Web.HttpContext.Current.Session[name: CartSessionKey] = System.Web.HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    Guid temp = Guid.NewGuid();
                    System.Web.HttpContext.Current.Session[name: CartSessionKey] = temp.ToString();
                }
            }
            return System.Web.HttpContext.Current.Session[name: CartSessionKey].ToString();
        }
    }
}
