using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
            
     
        }
        
        [ChildActionOnly]
       public string DateNow()
        {
            return DateTime.Now.ToShortDateString();
        }
    }
}