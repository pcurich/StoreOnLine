using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreOnLine.Areas.Management.Models;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Concrete;
using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Model.Resources;

namespace StoreOnLine.Areas.Management.Controllers
{
    [Authorize]
    public class AdminProductBaseController : Controller
    {
        private readonly IProductsRepository _repository;
        private readonly ImagenRepository _repositoryImagen;
        private readonly ICategoryRepository _repositoryCategory;
        private readonly ICampaingRepository _repositoryCampaing;

        public AdminProductBaseController(IProductsRepository repo, ICategoryRepository repositoryCategory, ICampaingRepository repositoryCampaing, ImagenRepository repositoryImagen)
        {
            ViewBag.Big = "Administrar: Productos Base";
            ViewBag.Small = "Configuracion de Categorias de Productos Base";
            ViewBag.Area = "Management";
            ViewBag.Controller = "AdminProductBase";
            ViewBag.Action = "Index";
            _repository = repo;
            _repositoryCategory = repositoryCategory;
            _repositoryCampaing = repositoryCampaing;
            _repositoryImagen = repositoryImagen;
        }

        public ViewResult Index()
        {
            var db = _repository.ProductBases.Where(o => !o.IsDeleted);
            var view = db.Select(productBase => new ProductBaseView().ToView(productBase)).ToList();
            return View(view);
        }

        public ViewResult Create()
        {
            ViewBag.Categories = GetCategories();
            ViewBag.ProductUnit = GetCategories();
            ViewBag.GetCampaings = GetCampaings();
            ViewBag.ProductSupplier = GetCategories();
            return View("Edit",  new ProductBaseView {ProductImagens = new List<Imagen>()});
        }

        public ViewResult Edit(int productId)
        {
            ViewBag.Action = "Edit";
            ViewBag.Categories = GetCategories();
            ViewBag.ProductUnit = GetCategories();
            ViewBag.GetCampaings = GetCampaings();
            ViewBag.ProductSupplier = GetCategories();


            var product = _repository.ProductBases.FirstOrDefault(p => p.Id == productId);
            if (product != null) ViewBag.ProductBase = product.ProductName;

            return View(new ProductBaseView().ToView(product));
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
                    imagen.ObjectName = product.ProductName;
                    imagen.ObjectId = product.Id;

                    if (product.Id == 0)
                    {
                        product.ProductImagens = new List<Imagen>();
                        imagen.Secuence = 1;
                    }
                    else
                    {
                        var firstOrDefault = _repository.ProductBases.FirstOrDefault(o => o.Id == product.Id);
                        if (firstOrDefault != null)
                        {
                            product.ProductImagens = firstOrDefault.ProductImagens;
                            for (var i = 0; i < product.ProductImagens.Count; i++)
                            {
                                product.ProductImagens[i].IsPrincipal = false;
                                product.ProductImagens[i].Secuence = i;
                                _repositoryImagen.SaveImagen(product.ProductImagens[i]);
                            }
                            imagen.Secuence = product.ProductImagens.Count;
                        }
                    }
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

        #region Custom

        private List<SelectListItem> GetCategories()
        {
            var repo = _repositoryCategory.Categories.Where(o => o.IsStatus).ToList();
            return repo.Select(r => new SelectListItem { Text = r.CategoryName, Value = Convert.ToString(r.Id) }).ToList();
        }

        private List<SelectListItem> GetCampaings()
        {
            var repo = _repositoryCampaing.Campaigns.Where(o => o.IsStatus).ToList();
            return repo.Select(r => new SelectListItem { Text = r.CampaignName, Value = Convert.ToString(r.Id) }).ToList();
        }
        #endregion
    }
}