using System.Data.Entity;
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
        public CategoryController(IUnitOfWork service) : base(service)
        {
            DummySeo("Categorias");
        }
        //
        // GET: /Catalog/Category/
        public ActionResult Index()
        {
            CreateBreadCrumb("Categorias", "Lista de categorias", "Catalog", "Category", "Index");
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var categoryLangs = Service.CategoryLangRepository.
                GetCategoryLangForCultura(Service.LanguageRepository.GetCurrentLanguage().Id).ToList();

            foreach (var categoryLang in categoryLangs)
            {
                categoryLang.CategoryRols = null;
            }
            return Json(categoryLangs.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, CategoryLang category)
        {
            if (category != null && ModelState.IsValid)
            {
                category.LanguageId = Service.LanguageRepository.GetCurrentLanguage().Id;//todo
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
            var categoryRol = Service.CategoryRolRepository.GetAll().Include(p => p.Rol).Where(o => o.CategoryLangId == categoryId).ToList();

            category.CategoryRols = categoryRol;

            if (categoryRol.Any())
            {
                return View(category);
            }
            //si no esta registrado en base de datos lo inserto
            var rols = Service.RolRepository.GetRolByLanguage(Service.LanguageRepository.GetCurrentLanguage().Id).ToList();
            category.AddRols(rols, categoryId);
            Service.Commit();
            DummySeo("Categoria: " + category.Name);
            return View(category);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Detail(CategoryLang model, FormCollection form)
        {
            if (model != null && ModelState.IsValid)
            {
                //var old = Service.CategoryLangRepository.GetById(model.Id);
                //model.Language = old.Language;

                foreach (var categoryRol in model.CategoryRols)
                {
                    Service.CategoryRolRepository.Update(categoryRol);
                }

                model.CategoryRols = null;
                Service.CategoryLangRepository.Update(model);
                Service.Commit();

                if ("Guardar".Equals(Request.Form["save"]))
                {
                    return View(Service.CategoryLangRepository.GetById(model.Id));
                }
            }

            return Redirect("Index");
            
        }
    }
}