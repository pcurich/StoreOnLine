using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Model.Resources;

namespace StoreOnLine.Areas.Management.Controllers
{
    [Authorize]
    public class AdminProductBaseController : Controller
    {
        private readonly IProductsRepository _repository;

        public AdminProductBaseController(IProductsRepository repo)
        {
            _repository = repo;
        }

        public ViewResult Index()
        {
            return View(_repository.ProductBases);
        }

        public ViewResult Edit(int productId)
        {
            ProductBase product = _repository.ProductBases.FirstOrDefault(p => p.Id == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(ProductBase product, HttpPostedFileBase image = null)
        {

            if (ModelState.IsValid)
            {
                var imagen = new Imagen();
                if (image != null)
                {
                    imagen.ImageMimeType = image.ContentType;
                    imagen.ImageData = new byte[image.ContentLength];
                    product.ProductImagens.Add(imagen);

                    image.InputStream.Read(imagen.ImageData, 0, image.ContentLength);
                }
                _repository.SaveProductBase(product);
                TempData["message"] = string.Format("{0} has been saved", product.ProductName);
                return RedirectToAction("Index");
            }
            // there is something wrong with the data values
            return View(product);
        }

        public ViewResult Create()
        {
            return View("Edit", new ProductBase());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = _repository.DeleteProductBase(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedProduct.ProductName);
            }
            return RedirectToAction("Index");
        }

    }
}