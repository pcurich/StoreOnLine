using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;
using StoreOnLine.Infrastructure;

namespace StoreOnLine
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();//Attribute Routing

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                            new
                            {
                                controller = "Home",
                                action = "Index",
                                id = UrlParameter.Optional
                            });

            //routes.MapRoute("ChromeRoute", "{*catchall}",
            //                new { controller = "Home", action = "Index" },
            //                new { customConstraint = new UserAgentConstraint("Chrome") },
            //                new[] { "StoreOnLine.Controllers" }
            //                );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}/{*catchall}",
            //    defaults: new
            //    {
            //        controller = "Home",
            //        action = "Index",
            //        id = UrlParameter.Optional
            //    },
            //    namespaces: new[] { "StoreOnLine.Controllers" }
            //).DataTokens["UseNamespaceFallback"] = false; //No find in otrher controllers


            //routes.MapRoute(
            //    name: "Default1",
            //    url: "{controller}/{action}/{id}/{*catchall}",
            //    defaults: new
            //    {
            //        controller = "Home",
            //        action = "Index",
            //        id = UrlParameter.Optional,
            //    },
            //    constraints: new
            //    {
            //        controller = "^H.*",
            //        action = "Index|About",
            //        httpMethod = new HttpMethodConstraint("GET"),
            //        id = new CompoundRouteConstraint(new IRouteConstraint[] {
            //            new AlphaRouteConstraint(),
            //            new MinLengthRouteConstraint(6)
            //            })
            //    },
            //    namespaces: new[] { "StoreOnLine.Controllers1" }
            //);

            /*
             * Constraining a Route Using a Regular Expression
               new { controller = "^H.*" }, the value of the controller variable begins with the letter H.
             * 
             * Constraining a Route to a Set of Specific Values
             * new { controller = "^H.*", action = "^Index$|^About$" }, allow the route to match only URLs where the value of the action segment is Index or About.
             * 
             * Constraining a Route Using HTTP Methods
             * new {
                    controller = "^H.*", action = "Index|About",
                    httpMethod = new HttpMethodConstraint("GET") o tmb httpMethod = new HttpMethodConstraint("GET", "POST") },
                    },
             * HTTP method constraint is slightly odd.
             * 
             * 
             * Name Description Attribute Constraint
                AlphaRouteConstraint() Matches alphabet characters, irrespective of case
                (A–Z, a–z)
                alpha
                BoolRouteConstraint() Matches a value that can be parsed into a bool                        bool
                DateTimeRouteConstraint() Matches a value that can be parsed into a DateTime                datetime
                DecimalRouteConstraint() Matches a value that can be parsed into a decimal                  decimal
                DoubleRouteConstraint() Matches a value that can be parsed into a double                    double
                FloatRouteConstraint() Matches a value that can be parsed into a float                      float
                IntRouteConstraint() Matches a value that can be parsed into an int                         int
             * 
                LengthRouteConstraint(len)
                LengthRouteConstraint(min, max)
                Matches a value with the specified number of
                characters or that is between min and max characters in length.                             length(len)  length(min, max)
                
                LongRouteConstraint() Matches a value that can be parsed into a long                        long
                MaxRouteConstraint(val) Matches an int value if the value is less than val                  max(val)
                MaxLengthRouteConstraint(len) Matches a string with no more than len characters             maxlength(len)
                MinRouteConstraint(val) Matches an int value if the value is more than val                  min(val)
                MinLengthRouteConstraint(len) Matches a string with at least len characters                 minlength(len)
                RangeRouteConstraint(min, max) Matches an int value if the value is between min and max     range(min, max) 
             * 
             * */
        }
    }
}
