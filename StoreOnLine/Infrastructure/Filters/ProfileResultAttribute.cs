using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreOnLine.Infrastructure.Filters
{
    public class ProfileResultAttribute : FilterAttribute, IResultFilter
    {
        /*
            When I apply a result filter to an action method, the OnResultExecuting
            method is invoked after the action method has returned an action result but before the action result is executed. The
            OnResultExecuted method is invoked after the action result is executed.
         */

        private Stopwatch _timer;
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            _timer = Stopwatch.StartNew();
        }
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _timer.Stop();
            filterContext.HttpContext.Response.Write(
            string.Format("<div>Result elapsed time: {0:F6}</div>",
            _timer.Elapsed.TotalSeconds));
        }

    }
}