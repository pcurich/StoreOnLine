using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreOnLine.Infrastructure.Filters
{
    public class ProfileAllAttribute : ActionFilterAttribute
    {
        private Stopwatch _timer;
        private readonly IDictionary<String, string> _results = new Dictionary<string, string>();
        private String _controller;
        private String _action;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _timer = Stopwatch.StartNew();
            _controller = filterContext.Controller.ToString();
            _action = filterContext.ActionDescriptor.ActionName;
            _results.Add(_controller + "-" + _action + " - OnActionExecuting", string.Format("{0:F6}", _timer.Elapsed.TotalSeconds));

        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _results.Add(_controller + "-" + _action + " - OnActionExecuted", string.Format("{0:F6}", _timer.Elapsed.TotalSeconds));
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            _results.Add(_controller + "-" + _action + " - OnResultExecuting", string.Format("{0:F6}", _timer.Elapsed.TotalSeconds));
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _timer.Stop();
            _results.Add(_controller + "-" + _action + " - OnResultExecuted", string.Format("{0:F6}", _timer.Elapsed.TotalSeconds));
            var report = string.Format("<div>Total elapsed time: {0:F6}</div>", _timer.Elapsed.TotalSeconds);

            report += "<table>";

            foreach (var result in _results)
            {
                report += "<tr><td>" + result.Key + "</td><td>" + result.Value + "</td></tr>";
            }

            report += "</table>";

            filterContext
                .HttpContext
                .Response
                .Write(report);
            _results.Clear();

        }
    }
}