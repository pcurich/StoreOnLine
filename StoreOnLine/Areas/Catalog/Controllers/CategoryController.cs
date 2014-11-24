using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using StoreOnLine.Controllers;
using StoreOnLine.DataBase.Data;
using StoreOnLine.DataBase.Model.CmsCategory;
using StoreOnLine.DataBase.Model.CmsLanguage;
using System.Linq;
using System.Web.Mvc;

namespace StoreOnLine.Areas.Catalog.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IQueryable<CategoryLang> _categories;
        private readonly Language _currentLanguage;

        public CategoryController(IUnitOfWork service)
            : base(service)
        {
            _currentLanguage = Service.LanguageRepository.GetCurrentLanguage();
            _categories = Service.CategoryLangRepository.GetCategoryLangForCultura(_currentLanguage.Id);

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
            return Json(_categories.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, CategoryLang category)
        {
            if (category != null && ModelState.IsValid)
            {
                category.LanguageId = _currentLanguage.Id;//todo
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

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int categoryId)
        {

            var category = Service.CategoryLangRepository.GetById(categoryId);
            var categoryRol = Service.CategoryRolRepository.GetAll().Where(o => o.CategoryLangId == categoryId);
            var rols = Service.RolRepository.GetRolByLanguage(_currentLanguage.Id);
            category.AddRols(rols, categoryId);
            
            if (categoryRol.Any())
            {
                return View(category);
            }
            //si no esta registrado en base de datos lo inserto
            Service.Commit();

            return View(category);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Detail(CategoryLang model)
        {
            if (model != null && ModelState.IsValid)
            {
                Service.CategoryLangRepository.Update(model);
                Service.Commit();
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateCategoryRol(int categoryId, int rolId)
        {
            var category =Service.CategoryRolRepository.GetAll()
                .First(o => o.CategoryLangId == categoryId && o.RolId == rolId);
            category.HasPermition = !category.HasPermition;
            Service.Commit();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

    }
}