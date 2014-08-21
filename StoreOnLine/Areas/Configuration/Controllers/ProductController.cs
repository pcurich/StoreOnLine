using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreOnLine.DataBase.Abstract;

namespace StoreOnLine.Areas.Configuration.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository _repository;

        public ProductController(IProductsRepository productRepository)
        {
            _repository = productRepository;
        }

        // GET: /Configuration/Product/List
        public ActionResult List()
        {
            return View(_repository.Products);
        }
    }
}