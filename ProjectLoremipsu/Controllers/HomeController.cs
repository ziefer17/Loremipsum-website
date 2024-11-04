using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectLoremipsu.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult TestList()
        {
            return View();
        }
        public ActionResult Form()
        {
            return View();
        }
        public ActionResult TestContent()
        {
            return View();
        }
        public ActionResult Homepage()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}