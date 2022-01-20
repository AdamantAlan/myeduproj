using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Adel.Areas.Adel.Data;
namespace Adel.Areas.Adel.Controllers
{
    public class HomeController : Controller
    {
        // GET: Adel/Home
        [HttpGet]
        public ActionResult Index()
        {
            return PartialView("_Index");
        }
        [HttpPost]
        public ActionResult Index(string password)
        {
            IAuthentificationAdel auAdel = new AuthentificationAdel();
            string authentificator = auAdel.Authentification(password);
            if (authentificator == "Access")
            {
                IResponceToTime responceToTime = new ResponceToTime();
                ViewBag.Welcome = responceToTime.Responce;
                return View("AuthentificationAdel");
            }
            else
                return PartialView("_Index");
        }
    }
}