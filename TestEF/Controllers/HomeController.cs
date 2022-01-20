using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestEF.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            using (ModelUserContainer db = new ModelUserContainer())
            {
                db.Entity1Set.Add(new Entity1() { Id = 1, User = "Alan" });
                db.SaveChanges();
                var q = db.Entity1Set.Where(i => i.Id == 1).ToList();
                if (q.Any(i => i.Id == 1))
                {
                    foreach (var item in q)
                        ViewBag.Data = $"{item.Id} {item.User}";
                }
                return View();
            }
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