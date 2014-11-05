using System;
using System.Web;

namespace StoreOnLine.Service.Helpers
{
    public class UrlHelpers
    {
        /// <summary>
        /// obtiene el url de forma completa con el  host incluido puerto y query
        /// </summary>
        /// <returns>string url</returns>
        public static string GetAbsoluteUri()
        {
            return HttpContext.Current.Request.Url.AbsoluteUri;
        }

        /// <summary>
        /// obtiene el url relativo con los valores del route. no incluye query
        /// </summary>
        /// <returns>string</returns>
        public static string GeAbsolutePath()
        {
            return HttpContext.Current.Request.Url.AbsolutePath;
        }

        /// <summary>
        /// obtiene el url relativo con los valores del route. incluye valor y query
        /// </summary>
        /// <returns>string</returns>
        public static string GetPathAndQuery()
        {
            return HttpContext.Current.Request.Url.PathAndQuery;
        }

        /// <summary>
        /// obtiene querystring
        /// </summary>
        /// <param name="key">key querystring</param>
        /// <returns>value or -1 if not found</returns>
        public static string GetQuery(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            var value = HttpContext.Current.Request.QueryString[key];

            return string.IsNullOrEmpty(value) ? "-1" : value;
        }

        /// <summary>
        /// get route value
        /// </summary>
        /// <param name="key">route key</param>
        /// <returns>value or 0 if not found</returns>
        public static string GetRoute(string key)
        {
            var value = HttpContext.Current.Request.RequestContext.RouteData.Values[key] ?? 0;
            return (string) value;
        }
    }
}
