using System.Web;
using System.Web.Mvc;

namespace StoreOnLine.Controllers
{
    public class InfoController : Controller
    {
        //
        // GET: /Info/
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

        public HttpStatusCodeResult ResultHttpUnauthorizedResult()
        {
            return new HttpUnauthorizedResult("hola");
        }
	}
}