using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Adel
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Adel.Controllers" }
            );
            //routes.MapRoute(
            //    name: "Test",
            //    url: "{controller}/{action}/{x}/{y}",
            //    defaults: new { controller = "Home", action = "Author", x =0, y=0 },
            //    namespaces: new[] { "Adel.Controllers" }
            //);
        }
    }
}
