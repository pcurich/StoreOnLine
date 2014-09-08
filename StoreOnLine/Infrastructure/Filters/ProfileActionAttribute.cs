using System.Diagnostics;
using System.Web.Mvc;

namespace StoreOnLine.Infrastructure.Filters
{
    public class ProfileActionAttribute : FilterAttribute, IActionFilter
    {
        /*
            The OnActionExecuting method is called before the action method is invoked. You can use this opportunity to
            inspect the request and elect to cancel the request, modify the request, or start some activity that will span the
            invocation of the action. The parameter to this method is an ActionExecutingContext object, which subclasses the
            ControllerContext class and defines the two additional properties
          
         */
        private Stopwatch _timer;

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _timer = Stopwatch.StartNew();
        }
        /*
            ActionDescriptor        ActionDescriptor Provides details of the action method
            Result                  ActionResult The result for the action method; a filter can cancel the request by setting
                                    this property to a non-null value
         
         */
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _timer.Stop();
            if (filterContext.Exception == null)
            {
                filterContext
                    .HttpContext
                    .Response
                    .Write(string.Format("<div>Action method elapsed time: {0:F6}</div>", _timer.Elapsed.TotalSeconds));
            }
        }

        /*
            ActionDescriptor    ActionDescriptor        Provides details of the action method
            Canceled            bool                    Returns true if the action has been canceled by another filter
            Exception           Exception               Returns an exception thrown by another filter or by the action method
            ExceptionHandled    bool                    Returns true if the exception has been handled
            Result              ActionResult            The result for the action method; a filter can cancel the request by
                                                        setting this property to a non-null value
         */
    }
}