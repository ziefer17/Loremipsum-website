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
    public class propertiesController : Controller
    {
        private QLBDSEntities db = new QLBDSEntities();

        // GET: properties
        public ActionResult Index()
        {
            var properties = db.properties.Include(p => p.listing_type).Include(p => p.property_location).Include(p => p.property_status).Include(p => p.property_type).Include(p => p.user).Include(p => p.images);
            return View(properties.ToList());
        }

        // GET: properties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            property property = db.properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: properties/Create
        public ActionResult Create()
        {
            ViewBag.listing_id = new SelectList(db.listing_type, "listing_id", "description");
            ViewBag.location_id = new SelectList(db.property_location, "location_id", "description");
            ViewBag.status_id = new SelectList(db.property_status, "status_id", "description");
            ViewBag.type_id = new SelectList(db.property_type, "type_id", "description");
            ViewBag.user_id = new SelectList(db.users, "user_id", "name");
            return View();
        }

        // POST: properties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "property_id,description,created_date,address,property_size,num_bedrooms,num_bathrooms,num_carspaces,price,user_id,status_id,type_id,listing_id,location_id")] property property)
        {
            if (ModelState.IsValid)
            {
                db.properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.listing_id = new SelectList(db.listing_type, "listing_id", "description", property.listing_id);
            ViewBag.location_id = new SelectList(db.property_location, "location_id", "description", property.location_id);
            ViewBag.status_id = new SelectList(db.property_status, "status_id", "description", property.status_id);
            ViewBag.type_id = new SelectList(db.property_type, "type_id", "description", property.type_id);
            ViewBag.user_id = new SelectList(db.users, "user_id", "name", property.user_id);
            return View(property);
        }

        // GET: properties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            property property = db.properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            ViewBag.listing_id = new SelectList(db.listing_type, "listing_id", "description", property.listing_id);
            ViewBag.location_id = new SelectList(db.property_location, "location_id", "description", property.location_id);
            ViewBag.status_id = new SelectList(db.property_status, "status_id", "description", property.status_id);
            ViewBag.type_id = new SelectList(db.property_type, "type_id", "description", property.type_id);
            ViewBag.user_id = new SelectList(db.users, "user_id", "name", property.user_id);
            return View(property);
        }

        // POST: properties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "property_id,description,created_date,address,property_size,num_bedrooms,num_bathrooms,num_carspaces,price,user_id,status_id,type_id,listing_id,location_id")] property property)
        {
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.listing_id = new SelectList(db.listing_type, "listing_id", "description", property.listing_id);
            ViewBag.location_id = new SelectList(db.property_location, "location_id", "description", property.location_id);
            ViewBag.status_id = new SelectList(db.property_status, "status_id", "description", property.status_id);
            ViewBag.type_id = new SelectList(db.property_type, "type_id", "description", property.type_id);
            ViewBag.user_id = new SelectList(db.users, "user_id", "name", property.user_id);
            return View(property);
        }

        // GET: properties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            property property = db.properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            property property = db.properties.Find(id);
            db.properties.Remove(property);
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
