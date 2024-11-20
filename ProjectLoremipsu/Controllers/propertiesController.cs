using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectLoremipsu.Models;
using PagedList;
using System.IO;

namespace ProjectLoremipsu.Controllers
{
    public class propertiesController : Controller
    {
        private QLBDSEntities db = new QLBDSEntities();
        public ActionResult History(int? page)
        {
            if (Session["UserEmail"] != null && Session["UserName"] != null)
            {
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                string userName = (string)Session["UserName"];
                var properties = db.properties.Include(p => p.listing_type).Include(p => p.property_location).Include(p => p.property_status)
                    .Include(p => p.property_type).Include(p => p.user)
                    .Include(p => p.images)
                    .Where(p=> p.user.name ==userName).OrderBy(p => p.property_id).ToList();
                
                return View(properties.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Index", "properties");
            }            
        }
        public ActionResult List(int? page)
        {
            if (Session["UserEmail"] != null)
            {
                int pageSize = 6;
                int pageNumber = (page ?? 1); ;
                var properties = db.properties.Include(p => p.listing_type).Include(p => p.property_location).Include(p => p.property_status)
                    .Include(p => p.property_type).Include(p => p.user)
                    .Include(p => p.images);
                properties = properties.OrderBy(p => p.property_id);
                return View(properties.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Index", "properties");
            }
        }
        // GET: properties
        public ActionResult Index(string valueSearch,int? searchLocate, int? page)
        {
            
            
            int pageSize = 6;
            int pageNumber = (page ?? 1); ;
            ViewBag.searchLocate = new SelectList(db.property_location, "location_id", "description", searchLocate);
            var properties = db.properties.Include(p => p.listing_type).Include(p => p.property_location).Include(p => p.property_status)
                .Include(p => p.property_type).Include(p => p.user)
                .Include(p => p.images)
                .AsQueryable();
            
            if (!string.IsNullOrEmpty(valueSearch))
            {
                properties = properties.Where(p => p.address.Contains(valueSearch)
                || p.user.name.Contains(valueSearch)
                || p.description.Contains(valueSearch)
                || p.property_location.description.Contains(valueSearch)
                || p.property_type.description.Contains(valueSearch));
                                 
            }
            if (searchLocate.HasValue)
            {
                properties = properties.Where(p => p.location_id == searchLocate.Value);
            }
            properties = properties.OrderBy(p => p.property_id);
            return View(properties.ToPagedList(pageNumber, pageSize));            
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
        [HttpGet]
        public ActionResult Create()
        {

            ViewBag.listing_id = new SelectList(db.listing_type, "listing_id", "description");
            ViewBag.location_id = new SelectList(db.property_location, "location_id", "description");
            ViewBag.status_id = new SelectList(db.property_status, "status_id", "description");
            ViewBag.type_id = new SelectList(db.property_type, "type_id", "description");
            ViewBag.user_id = new SelectList(db.users, "user_id", "name");
            return View();
            //var viewModel = new PropertyViewModel
            //{
            //    Property = new property() // Ensure Property is initialized
            //};
            //return View(viewModel);
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
                property.property_id = new Random().Next();
                property.created_date = DateTime.Now;
                property.user_id = 1;
                db.properties.Add(property);

                db.SaveChanges();
                return RedirectToAction("Homepage", "Home");
            }

            ViewBag.listing_id = new SelectList(db.listing_type, "listing_id", "description", property.listing_id);
            ViewBag.location_id = new SelectList(db.property_location, "location_id", "description", property.location_id);
            ViewBag.status_id = new SelectList(db.property_status, "status_id", "description", property.status_id);
            ViewBag.type_id = new SelectList(db.property_type, "type_id", "description", property.type_id);

            //ViewBag.user_id = new SelectList(db.users, "user_id", "name", property.user_id);
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
