using System.Web.Mvc;

namespace StoreOnLine.Infrastructure
{
    public class CustomRedirectResult : ActionResult
    {
        public string Url { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var fullUrl = UrlHelper.GenerateContentUrl(Url, context.HttpContext);
            context.HttpContext.Response.Redirect(fullUrl);
        }
        /*
        ViewResult              Renders the specified or default view template                          View
        PartialViewResult       Renders the specified or default partial view template                  PartialView
        RedirectToRouteResult   Issues an HTTP 301 or 302 redirection to an action method or specific   RedirectToAction
                                route entry, generating a URL according to your routing configuration   RedirectToActionPermanent
                                                                                                        RedirectToRoute
                                                                                                        RedirectToRoutePermanent

        RedirectResult          Issues an HTTP 301 or 302 redirection to a specific URL                 Redirect
                                                                                                        RedirectPermanent
        ContentResult           Returns raw textual data to the browser, optionally                     Content
                                setting a content-type header
        FileResult              Transmits binary data (such as a file from disk or a byte               File
                                array in memory) directly to the browser
        
        JsonResult              Serializes a .NET object in JSON format and sends it as                 Json
                                the response. This kind of response is more typically
                                generated using the Web API feature, which I describe
                                in Chapter 27, but you can see this action type used in
                                Chapter 23.
        
        JavaScriptResult        Sends a snippet of JavaScript source code that should                   JavaScript
                                be executed by the browser
        
        HttpUnauthorizedResult  Sets the response HTTP status code to 401 (meaning                      None
                                “not authorized”), which causes the active
                                authentication mechanism (forms authentication or
                                Windows authentication) to ask the visitor to log in
        
        HttpNotFoundResult      Returns a HTTP 404—Not found error                                      HttpNotFound
        HttpStatusCodeResult    Returns a specified HTTP code                                           None
        EmptyResult             Does nothing                                                            None
        
        */
    }
}