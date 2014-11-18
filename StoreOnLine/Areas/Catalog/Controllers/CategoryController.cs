using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using StoreOnLine.Controllers;
using StoreOnLine.DataBase.Data;
using StoreOnLine.DataBase.Model.CmsCategory;

namespace StoreOnLine.Areas.Catalog.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork service)
            : base(service)
        {
            Service.CategoryLangRepository.GetCategoryLangForCultura(1);

        }
        //
        // GET: /Catalog/Category/
        public ActionResult Index()
        {
            Service.Commit();
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(Service.CategoryLangRepository.GetAll().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, CategoryLang category)
        {
            if (category != null && ModelState.IsValid)
            {
                category.LanguageId = 149;//todo
                Service.CategoryLangRepository.Add(category);
            }
            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, CategoryLang category)
        {
            if (category != null && ModelState.IsValid)
            {
                Service.CategoryLangRepository.Update(category);
            }
            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, CategoryLang category)
        {
            if (category != null)
            {
                Service.CategoryLangRepository.Delete(category);
            }
            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            Service.Commit();
            base.Dispose(disposing);
        }

    }
}