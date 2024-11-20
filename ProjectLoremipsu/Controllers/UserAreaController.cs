using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectLoremipsu.Models;
using System.IO;

namespace ProjectLoremipsu.Controllers
{
    public class UserAreaController : Controller
    {
        QLBDSEntities db = new QLBDSEntities();
        
        public new ActionResult Profile()
        {

            if (Session["UserEmail"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        // GET: UserArea
        public ActionResult Index()
        {
            return View();
        }
    }
}