using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseShuffle.Data.Factory.FactoryData;

namespace CourseShuffle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SubscriptionPage(long departmentId)
        {
            var department = new DepartmentFactory().GetAppUserDepartmet(departmentId);
            return View(department);
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