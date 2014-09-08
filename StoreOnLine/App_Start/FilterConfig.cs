using System.Web.Mvc;
using StoreOnLine.Infrastructure.Filters;

namespace StoreOnLine
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ProfileAllAttribute());
        }
    }
}