using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using StoreOnLine.Infrastructure;

namespace StoreOnLine.Controllers
{
    //[Authorize(Roles = "trader")] // applies to all actions
    public class HomeController : Controller
    {

        //[ShowMessage] // applies to just this action
        //[OutputCache(Duration = 60)] // applies to just this action
        [HttpPost]
        public ActionResult Index(int i)
        {
            var remote = new RemoteService();
            return View("Index", (object)remote.GetRemoteData());
        }

        public async Task<ActionResult> Index()
        {
            var data = await Task<string>.Factory.StartNew(() => new RemoteService().GetRemoteData());
            return View("Index", (object)data);
        }

        public async Task<ActionResult> ConsumeAsyncMethod()
        {
            string data = await new RemoteService().GetRemoteDataAsync();
            return View("Index", (object)data);
        }

        //
        // GET: /Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [CustomAuth(false)]
        public ActionResult Create()
        {
            ViewBag.Controller = "Customer";
            ViewBag.Action = "Create";
            return View();
        }

        //
        // POST: /Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
            }
            else
            {
                ModelState.AddModelError("", "Incorrect username or password");
                return View();
            }
        }
    }

    public class RemoteService
    {
        public string GetRemoteData()
        {
            Thread.Sleep(20000);
            return "Hola";
        }

        public async Task<string> GetRemoteDataAsync()
        {
            return await Task<string>.Factory.StartNew(() =>
            {
                Thread.Sleep(20000);
                return "Hello from the other side of the world";
            });
        }
    }
}
