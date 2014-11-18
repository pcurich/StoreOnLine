using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Model.CmsEmploye;
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
            ModelBinders.Binders.Add(typeof(Employer), new EmployerModelBinder());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
