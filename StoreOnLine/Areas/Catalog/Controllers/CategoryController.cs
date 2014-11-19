using System.Linq;
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
                //Service.Commit();
                //var rols = Service.RolRepository.GetRolByLanguage(149);
                //foreach (var rol in rols)
                //{
                //    category.CategoryRols.Add(new CategoryRol { RolId = rol.Id, CategoryLangId = category.Id, Rol = rol });
                //}
                //Service.CategoryLangRepository.Update(category);
                //Service.Commit();
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

        public ActionResult Detail(int categoryId)
        {
            var category = Service.CategoryLangRepository.GetById(categoryId);
            var rols = Service.RolRepository.GetRolByLanguage(149);
            foreach (var rol in rols)
            {
                category.CategoryRols.Add(new CategoryRol { RolId = rol.Id, CategoryLangId = categoryId, Rol = rol});
            }
            return View(category);
        }

        public ActionResult ReadRol([DataSourceRequest] DataSourceRequest request)
        {
            return Json(Service.CategoryRolRepository.GetAll().ToDataSourceResult(request));
        }

        protected override void Dispose(bool disposing)
        {
            Service.Commit();
            base.Dispose(disposing);
        }

    }
}