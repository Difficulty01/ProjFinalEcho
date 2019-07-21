using Microsoft.AspNet.Identity.EntityFramework;
using ProjFinalEcho1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjFinalEcho1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /*using (var ctx = new DefaultConnection())
            {
                var student = (from s in ctx.Students
                               where s.StudentName == "Bill"
                               select s).FirstOrDefault<Student>();
            }*/

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