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
            context.MapRoute(
                name: null,
                url: "",
            defaults: new
            {
                AreaName = "Configuration",
                controller = "Product",
                action = "List",
                category = (string)null,
                page = 1
            });

            context.MapRoute(
               name: null,
               url: "Configuration/{controller}/{action}/Page{page}",
               defaults: new { controller = "Product", action = "List" }
           );

            context.MapRoute(
                "Configuration_default",
                "Configuration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}