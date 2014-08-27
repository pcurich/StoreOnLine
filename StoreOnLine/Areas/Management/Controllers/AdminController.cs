using System.Web.Mvc;
using StoreOnLine.DataBase.Abstract;

namespace StoreOnLine.Areas.Management.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductsRepository _repository;

        public AdminController(IProductsRepository repo)
        {
            _repository = repo;
        }

        public ViewResult IndexBase()
        {
            return View(_repository.ProductBases);
        }

        public ViewResult IndexComposite()
        {
            return View(_repository.ProductComposites);
        }
    }
}