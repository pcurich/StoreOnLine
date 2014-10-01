using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreOnLine.Areas.Merchant.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Merchant/Base/
        public ActionResult Index()
        {
            return View();
        }
	}
}