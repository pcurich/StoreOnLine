using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Security;
using StoreOnLine.DataBase.Model.Shopping;
using StoreOnLine.Infrastructure.Binders;

namespace StoreOnLine
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //Database.SetInitializer(new StoreOnLineContext.StoreOnLineInitializerDropCreateDatabaseAlways());
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            ModelBinders.Binders.Add(typeof(User), new UserModelBinder());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
