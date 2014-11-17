using System;
using System.Globalization;
using System.Web.Mvc;
using StoreOnLine.Controllers;
using StoreOnLine.DataBase.Model.CmsCategory;
using StoreOnLine.Service.Service.Categories;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace StoreOnLine.Areas.Catalog.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController()
        {
            ServCategory = ServCategory.Instance(1, 1);
        }
        //
        // GET: /Catalog/Category/
        public ActionResult Index()
        {
            ServCategory.GetAll();
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(ServCategory.GetAll().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, CategoryLang category)
        {
            if (category != null && ModelState.IsValid)
            {
                ServCategory.AddCategory(category);
            }
            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, CategoryLang category)
        {
            if (category != null && ModelState.IsValid)
            {
                ServCategory.Update(category);
            }
            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, CategoryLang category)
        {
            if (category != null)
            {
                ServCategory.Delete(category);
            }
            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }
    }
}