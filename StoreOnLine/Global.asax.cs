using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //Database.SetInitializer(new StoreOnLineContext.StoreOnLineInitializerDropCreateDatabaseAlways());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
