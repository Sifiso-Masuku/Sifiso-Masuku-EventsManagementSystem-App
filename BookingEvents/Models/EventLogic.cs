using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookingEvents.Model;

namespace BookingEvents.Models
{
    public class EventLogic
    {
        private ApplicationDbContext db;
        public EventLogic()
        {
            this.db = new ApplicationDbContext();
        }
        public List<Event_Type> GetEvent_s()
        {
            return db.Events.ToList();
        }
        public bool Add(Event_Type event_Type)
        {
            try
            {
                db.Events.Add(event_Type);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
            //public List<Artist> GetArtirst_s()
            //{
            //    return db.Artists.ToList();
            //}
            //public bool Add(Artist Artist)
            //{
            //    try
            //    {
            //        db.Events.Add(event_Type);
            //        db.SaveChanges();
            //        return true;
            //    }
            //    catch (Exception ex)
            //    {
            //        return false;
            //    }

        
