using BookingEvents.Models;
using BookingEvents.Models.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookingEvents.Shop.Logic
{
    public class Item_Business
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Item> all()
        {
            return db.Items.Include(i => i.Department).ToList();
        }
        public bool add(Item model)
        {
            try
            {
                db.Items.Add(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool edit(Item model)
        {
            try
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool delete(Item model)
        {
            try
            {
                db.Items.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public Item find_by_id(int? id)
        {
            return db.Items.Find(id);
        }
        public void updateStock_Received(int item_id, int quantity)
        {
            var item = db.Items.Find(item_id);
            item.QuantityInStock += quantity;
            db.SaveChanges();
        }
        public void updateStock_bot(int item_id, int quantity)
        {
            var item = db.Items.Find(item_id);
            item.QuantityInStock -= quantity;
            db.SaveChanges();
        }
        //public void updateOrder(int id, double price)
        //{
        //    var item = db.Order_Items.Find(id);
        //    item.price = price;
        //    item.replied = true;
        //    item.date_replied = DateTime.Now;
        //    item.status = "Supplier Replied with Pricing Details";
        //    db.SaveChanges();
        //}

    }
}