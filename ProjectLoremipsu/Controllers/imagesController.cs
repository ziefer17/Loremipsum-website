using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectLoremipsu.Models;

namespace ProjectLoremipsu.Controllers
{
    public class imagesController : Controller
    {
        private QLBDSEntities db = new QLBDSEntities();

        // GET: images
        public ActionResult Index()
        {
            var images = db.images.Include(i => i.property);
            return View(images.ToList());
        }

        // GET: images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            image image = db.images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // GET: images/Create
        public ActionResult Create()
        {
            ViewBag.property_id = new SelectList(db.properties, "property_id", "description");
            return View();
        }

        // POST: images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "image_id,img1,img2,img3,img4,property_id")] image image)
        {
            if (ModelState.IsValid)
            {
                db.images.Add(image);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.property_id = new SelectList(db.properties, "property_id", "description", image.property_id);
            return View(image);
        }

        // GET: images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            image image = db.images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            ViewBag.property_id = new SelectList(db.properties, "property_id", "description", image.property_id);
            return View(image);
        }

        // POST: images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "image_id,img1,img2,img3,img4,property_id")] image image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.property_id = new SelectList(db.properties, "property_id", "description", image.property_id);
            return View(image);
        }

        // GET: images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            image image = db.images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            image image = db.images.Find(id);
            db.images.Remove(image);
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
