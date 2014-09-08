using System;
using System.Web;
using System.Web.Mvc;
using StoreOnLine.Infrastructure;
using StoreOnLine.Infrastructure.Filters;

namespace StoreOnLine.Controllers
{
    public class InfoController : Controller
    {
        //
        // GET: /Info/
        [SimpleMessage(Message = "A", Order = 1)]
        [SimpleMessage(Message = "B", Order = 2)]
        public ActionResult Index()
        {
            var queryString = Request.QueryString;
            var form = Request.Form;
            var cookies = Request.Cookies;
            var httpMethod = Request.HttpMethod;
            var headers = Request.Headers;
            var url = Request.Url;
            var userHostAddress = Request.UserHostAddress;
            var route = RouteData.Route;
            var values = RouteData.Values;
            var application = HttpContext.Application;
            var cache = HttpContext.Cache;
            var items = HttpContext.Items;
            var session = HttpContext.Session;
            var user = User;
            var tempData = TempData;


            return View();
        }
        public void ProduceOutput()
        {
            if (Server.MachineName == "TINY")
            {
                Response.Redirect("/Basic/Index");
            }
            else
            {
                Response.Write("Controller: Derived, Action: ProduceOutput");
            }
        }

        public RedirectToRouteResult Redirect1()
        {
            return RedirectToRoute(new
            {
                controller = "Example",
                action = "Index",
                ID = "MyID"
            });
        }

        public RedirectResult Redirect2()
        {
            return RedirectPermanent("/Example/Index");
        }

        public RedirectResult Redirect3()
        {
            return Redirect("/Example/Index");
        }

        public HttpStatusCodeResult StatusCode()
        {
            return new HttpStatusCodeResult(404, "URL cannot be serviced");
        }

        public HttpStatusCodeResult ResultHttpNotFound()
        {
            return HttpNotFound("hola ");
        }


        [CustomAuthentication]
        [Authorize(Users = "bob@google.com")]
        public string List()
        {
            return "This is the List action on the Home controller";
        }
        public HttpStatusCodeResult ResultHttpUnauthorizedResult()
        {
            return new HttpUnauthorizedResult("hola");
        }

        //[RangeException]
        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "RangeError")]
        public string RangeTest(int id)
        {
            if (id > 100)
            {
                return String.Format("The id value is: {0}", id);
            }
            else
            {
                throw new ArgumentOutOfRangeException("id", id, "");
            }
        }

        [CustomAction]
        public string FilterTestA()
        {
            return "This is the FilterTest action";
        }

        //[ProfileAction]
        //[ProfileResult]
        [ProfileAll]
        public string FilterTestP()
        {
            return "This is the ActionFilterTest action";
        }
    }
}