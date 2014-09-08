using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.Web.Security;

namespace StoreOnLine.Infrastructure
{
    public class CustomAuthenticationAttribute : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            /*
                ActionDescriptor        Returns an ActionDescriptor that describes the action method to which the 
             *                          filter has been applied
                Principal               Returns an IPrincipal implementation that identifies the current user, 
             *                          if they have already been authenticated.
                Result                  Sets an ActionResult that expresses the result of the authentication check
             */
            IIdentity ident = filterContext.Principal.Identity;
            if (!ident.IsAuthenticated || !ident.Name.EndsWith("@google.com"))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {

            /*
                ActionDescriptor        Returns an ActionDescriptor that describes the action method to which 
             *                          the filter has been applied
                Result                  Sets an ActionResult that expresses the result of the authentication challenge
             */
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                if (filterContext.Result == null)   
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        {"controller", "GoogleAccount"},
                        {"action", "Login"},
                        {"returnUrl", filterContext.HttpContext.Request.RawUrl}
                    });
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                //clear the authentication for the request,
            }
  

        }
    }
}