using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AJAX.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BookSearch(string name)
        {

            if (name.Length <= 0)
            {
                return HttpNotFound();
            }
            return PartialView("BookSearch", name +"Hello");
        }
        public ActionResult BestBook()
        {
            string book = "Kniga";
            return PartialView("BestBook",book);
        }



    }
}