using System.Web;
using System.Web.Mvc;

namespace StoreOnLine.Infrastructure
{
    public class CustomAuthAttribute : AuthorizeAttribute
    {
        private readonly bool _localAllowed;

        public CustomAuthAttribute(bool allowedParam)
        {
            _localAllowed = allowedParam;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //permite el acceso a la maquina local
            return !httpContext.Request.IsLocal || _localAllowed;
        }
    }
}