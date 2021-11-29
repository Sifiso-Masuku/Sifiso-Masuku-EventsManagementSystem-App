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
    public class Venue1csController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Venue1cs
        public ActionResult Index()
        {
            return View(db.Venue.ToList());
        }
        public ActionResult Venues()
        {
            return View(db.Venue.ToList());
        }

        // GET: Venue1cs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue1cs venue1cs = db.Venue.Find(id);
            if (venue1cs == null)
            {
                return HttpNotFound();
            }
            return View(venue1cs);
        }

        // GET: Venue1cs/Create
        public ActionResult Create()
        {
            return View();
        }
        //Image
        // Convert file to binary
        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }
        //Image
        //Display File
        public FileStreamResult RenderImage(int id)
        {
            MemoryStream ms = null;

            var item = db.Venue.FirstOrDefault(x => x.venueId == id);
            if (item != null)
            {
                ms = new MemoryStream(item.Image);
            }
            return new FileStreamResult(ms, item.ImageType);
        }
        // POST: Venue1cs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "venueId,venueName,Address,Cost,Image,ImageType,NumGests")] Venue1cs venue1cs, HttpPostedFileBase img_upload)
        {
            //Image
            if (img_upload != null && img_upload.ContentLength > 0)
            {
                venue1cs.ImageType = Path.GetExtension(img_upload.FileName);
                venue1cs.Image = ConvertToBytes(img_upload);
            }
            //
            var ve = db.Venue.Where(p => p.venueName == venue1cs.venueName).Count();
            if (ve != 0)
            {
                TempData["AlertMessage"] = "Cannot add " + venue1cs.venueName + " because it already exists";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Venue.Add(venue1cs);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
          
            return View(venue1cs);
        }

        // GET: Venue1cs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue1cs venue1cs = db.Venue.Find(id);
            if (venue1cs == null)
            {
                return HttpNotFound();
            }
            return View(venue1cs);
        }

        // POST: Venue1cs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "venueId,venueName,Address,Cost")] Venue1cs venue1cs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venue1cs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(venue1cs);
        }

        // GET: Venue1cs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue1cs venue1cs = db.Venue.Find(id);
            if (venue1cs == null)
            {
                return HttpNotFound();
            }
            return View(venue1cs);
        }

        // POST: Venue1cs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Venue1cs venue1cs = db.Venue.Find(id);
            db.Venue.Remove(venue1cs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public PartialViewResult Venue()
        {
            string[] arr = { "Event Type", "Venue" };
            return PartialView("_Venue", arr);
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
