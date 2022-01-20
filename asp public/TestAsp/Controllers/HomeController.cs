using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestAsp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string mes)
        {
            ViewBag.mes = mes;
            return View();
        }
        [ChildActionOnly]
        public ActionResult IndexAj(string id)
        {
          
            return PartialView("IndexAj",id);
        }
        public ActionResult GoToFicBook()
        {
            return Redirect("https://ficbook.net/authors/1154790");
        }

        [ChildActionOnly]
        public ActionResult About()
        {
            return PartialView("_About",DateTime.Now.ToString());
        }

 
    }
}