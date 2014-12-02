using System.Web.Mvc;
using StoreOnLine.Controllers;
using StoreOnLine.DataBase.Data;

namespace StoreOnLine.Areas.Catalog.Controllers
{
    public class ProductsController : BaseController
    {
        //
        // GET: /Catalog/Products/
        public ProductsController(IUnitOfWork service) : base(service)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
	}
}