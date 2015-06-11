using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FileCabinet
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "Articles",
                defaults: new { controller = "Articles", action = "List", category = (string)null, page = 1 });

            routes.MapRoute(
                name: null,
                url: "Articles/page{page}",
                defaults: new { controller = "Articles", action = "List", category = (string)null },
                constraints: new { page = @"\d+" });

            routes.MapRoute(
                name: null,
                url: "Articles/Category/{category}",
                defaults: new { controller = "Articles", action = "List", page = 1 });

            routes.MapRoute(
                name: null,
                url: "Articles/{category}/page{page}",
                defaults: new { controller = "Articles", action = "List" },
                constraints: new { page = @"\d+" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
