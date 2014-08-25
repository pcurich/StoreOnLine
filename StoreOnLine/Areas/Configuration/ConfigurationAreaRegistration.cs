using System.Web.Mvc;

namespace StoreOnLine.Areas.Configuration
{
    public class ConfigurationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Configuration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(null,
            //     "Page{page}",
            //     new { controller = "Product", action = "List", category = (string)null },
            //     new { page = @"\d+" }
            // );

            //context.MapRoute("Configuration_Category",
            //    "{category}",
            //    new { controller = "Product", action = "List", page = 1 }
            //);

            //context.MapRoute("Configuration_Category_Page",
            //    "{category}/Page{page}",
            //    new { controller = "Product", action = "List" },
            //    new { page = @"\d+" }
            //);

            context.MapRoute(
                "Configuration_default",
                "Configuration/{controller}/{action}/{category}/{page}",
                new { controller = "ProductBase", action = "List", category = (string)null, page = UrlParameter.Optional }
            );
        }
    }
}