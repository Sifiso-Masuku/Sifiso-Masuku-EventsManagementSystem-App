using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookingEvents.Models;

namespace BookingEvents.Controllers
{
    public class Event_TypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Event_Type
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        // GET: Event_Type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_Type event_Type = db.Events.Find(id);
            if (event_Type == null)
            {
                return HttpNotFound();
            }
            return View(event_Type);
        }
        //Image
        //// Convert file to binary
        //public byte[] ConvertToBytes(HttpPostedFileBase file)
        //{
        //    BinaryReader reader = new BinaryReader(file.InputStream);
        //    return reader.ReadBytes((int)file.ContentLength);
        //}
        //Image
        //Display File
        //public FileStreamResult RenderImage(int id)
        //{
        //    MemoryStream ms = null;

        //    var item = db.Events.FirstOrDefault(x => x.EventId == id);
        //    if (item != null)
        //    {
        //        ms = new MemoryStream(item.Image);
        //    }
        //    return new FileStreamResult(ms, item.ImageType);
        //}
        // GET: Event_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,Image,EventName,BasicPrice")] Event_Type event_Type /*HttpPostedFileBase img_upload*/)
        {
            ////Image
            //if (img_upload != null && img_upload.ContentLength > 0)
            //{
            //    event_Type.ImageType = Path.GetExtension(img_upload.FileName);
            //    event_Type.Image = ConvertToBytes(img_upload);
            //}
            //
            if (ModelState.IsValid)
            {
                db.Events.Add(event_Type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(event_Type);
        }

        // GET: Event_Type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_Type event_Type = db.Events.Find(id);
            if (event_Type == null)
            {
                return HttpNotFound();
            }
            return View(event_Type);
        }

        // POST: Event_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,EventName,BasicPrice")] Event_Type event_Type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(event_Type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(event_Type);
        }

        // GET: Event_Type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_Type event_Type = db.Events.Find(id);
            if (event_Type == null)
            {
                return HttpNotFound();
            }
            return View(event_Type);
        }

        // POST: Event_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event_Type event_Type = db.Events.Find(id);
            db.Events.Remove(event_Type);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
