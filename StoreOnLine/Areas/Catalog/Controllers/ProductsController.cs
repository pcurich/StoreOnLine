using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using StoreOnLine.Controllers;
using StoreOnLine.DataBase.Data;
using StoreOnLine.DataBase.Model.CmsProduct;

namespace StoreOnLine.Areas.Catalog.Controllers
{
    public class ProductsController : BaseController
    {
        //
        // GET: /Catalog/Products/
        public ProductsController(IUnitOfWork service)
            : base(service)
        {
            DummySeo("Productos");
        }

        public ActionResult Index()
        {
            CreateBreadCrumb("Productos", "Lista de productos", "Catalog", "Products", "Index");
            return View();
        }


        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var productLangs = Service.ProductLangRepository.
                                        GetProductLangForCultura(
                                        Service.LanguageRepository.GetCurrentLanguage().Id
                                        ).ToList();

            return Json(productLangs.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, ProductLang product)
        {
            if (product != null && ModelState.IsValid)
            {
                product.LanguageId = Service.LanguageRepository.GetCurrentLanguage().Id;//todo
                Service.ProductLangRepository.Add(product);
                Service.Commit();
            }
            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, ProductLang product)
        {
            if (product != null && ModelState.IsValid)
            {
                Service.ProductLangRepository.Update(product);
                Service.Commit();
            }
            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, ProductLang product)
        {
            if (product != null)
            {
                Service.ProductLangRepository.Delete(product);
            }
            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

    }
}