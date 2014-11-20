using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using StoreOnLine.Controllers;
using StoreOnLine.DataBase.Data;
using StoreOnLine.DataBase.Model.CmsCategory;
using StoreOnLine.DataBase.Model.CmsRol;

namespace StoreOnLine.Areas.Catalog.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork service): base(service)
        {
            Service.CategoryLangRepository.GetCategoryLangForCultura(1);
        }
        //
        // GET: /Catalog/Category/
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(Service.CategoryLangRepository.GetAll().ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, CategoryLang category)
        {
            if (category != null && ModelState.IsValid)
            {
                category.LanguageId = 149;//todo
                Service.CategoryLangRepository.Add(category);
                Service.Commit();
            }
            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, CategoryLang category)
        {
            if (category != null && ModelState.IsValid)
            {
                Service.CategoryLangRepository.Update(category);
                Service.Commit();
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
            var categoryRol = Service.CategoryRolRepository.GetAll().Where(o => o.CategoryLangId == categoryId);
            var rols = Service.RolRepository.GetRolByLanguage(149);
            category.AddRols(rols, categoryId);

            if (categoryRol.Any())
            {
                return View(category);
            }
            
            Service.Commit();

            return View(category);
        }
 
    }
}