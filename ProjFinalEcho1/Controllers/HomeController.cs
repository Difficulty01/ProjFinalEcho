using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjFinalEcho1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index","Posts");
            //return View();index
        }

        public ActionResult About()
        {
            ViewBag.Message = "As suas noticias em primeira mão.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Como nos contactar.";

            return View();
        }
    }
}