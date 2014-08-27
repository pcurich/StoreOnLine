using StoreOnLine.DataBase.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StoreOnLine.Areas.Shopping.Controllers
{
    public class NavController : Controller
    {
        private readonly ICategoryRepository _repositoryCategory;
        private readonly ICampaingRepository _repositoryCampaing;

        public NavController(ICategoryRepository repocategory, ICampaingRepository repocompaing)
        {
            _repositoryCategory = repocategory;
            _repositoryCampaing = repocompaing;
        }

        public PartialViewResult MenuCategory(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = _repositoryCategory.Categories
                                                       .Select(x => x.CategoryName)
                                                       .Distinct()
                                                       .OrderBy(x => x);
            return PartialView("FlexMenuCategory",categories);
        }

        public PartialViewResult MenuCampaing(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = _repositoryCategory.Categories
                                                       .Select(x => x.CategoryName)
                                                       .Distinct()
                                                       .OrderBy(x => x);

            return PartialView(categories);
        }
    }
}