using System;
using System.Linq;
using System.Web.Mvc;
using StoreOnLine.Areas.Configuration.Models;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.Areas.Configuration.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository _repository;
        public int PageSize = 4;

        public ProductController(IProductsRepository productRepository)
        {
            _repository = productRepository;
        }

        // GET: /Configuration/Product/List
        public ViewResult List(String category, int page = 1)
        {
            var categoryId = Convert.ToInt32(category);
            var model = new ProductsListViewModel
            {
                Products = _repository.Products
                .Where(p => p.ProductCategoryId == categoryId)
                .OrderBy(p => p.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItem = _repository.Products.Count()
                },
                CurrentCategoryId = categoryId
            };
            return View(model);

        }
    }
}