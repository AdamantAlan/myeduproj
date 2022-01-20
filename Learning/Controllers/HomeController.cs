using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learning.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {
            double a = 10,b=0;
            double ab = a / b;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HandleError(ExceptionType = typeof(DivideByZeroException), View = "DError")]
        public ActionResult Content()
        {
           int a = 10, b = 0;
            double ab = a / b;
            return Content(ab.ToString());
        }




    }
}