using System;
using System.Threading;
using System.Web.Mvc;
using StoreOnLine.DataBase.Data;
using StoreOnLine.HtmlHelpers;
using StoreOnLine.Service.Service.Categories;
using StoreOnLine.Service.Service.Employers;
using StoreOnLine.Service.Service.Languages;
using StoreOnLine.Service.Service.Roles;

namespace StoreOnLine.Controllers
{
    public class BaseController : Controller
    {
        public IUnitOfWork Service;

        public BaseController(IUnitOfWork service)
        {
            Service = service;
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName;

            // Attempt to read the culture cookie from Request
            var cultureCookie = Request.Cookies["_culture"];

            if (cultureCookie != null)
            {
                cultureName = cultureCookie.Value;
            }
            else
            {
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0
                    ? Request.UserLanguages[0]
                    : // obtain it from HTTP header AcceptLanguages
                    null;
            }
            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            cultureName = CultureHelper.GetImplementedCulture(cultureName);
            RouteData.Values["culture"] = cultureName;

            ExecutedServices();

            return base.BeginExecuteCore(callback, state);
        }

        private void ExecutedServices()
        {
            //ServLanguage = new ServLanguage();
            //ServRol = new ServRol();
            var userName=System.Web.HttpContext.Current.User.Identity.Name;
            if (userName=="")
            {
                userName = "pcurich";
            }

           // ServEmployer = ServEmployer.Instance(userName);//Cambiar esto 
        }
    }
}