using FileCabinet.filters;
using FileCabinet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;

namespace FileCabinet
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //[InitializeSimpleMembershipAttribute]
        protected void Application_Start()
        {
            //Database.SetInitializer(new UsersInitializer());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserProfileId", "Username", true);
        }
    }
}
