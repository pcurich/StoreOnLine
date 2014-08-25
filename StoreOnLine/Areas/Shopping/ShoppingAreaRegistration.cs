using System.Web.Mvc;

namespace StoreOnLine.Areas.Shopping
{
    public class ShoppingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Shopping";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.MapRoute(
                "Shopping_default_base",
                "Shopping/{controller}/{action}/{category}/{page}",
                new { controller = "ProductBase", action = "List", category = (string)null, page = UrlParameter.Optional }
            );

            context.MapRoute(
                "Shopping_default_composite",
                "Shopping/{controller}/{action}/{category}/{page}",
                new { controller = "ProductComposite", action = "List", category = (string)null, page = UrlParameter.Optional }
            );

        }
    }
}