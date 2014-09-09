﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreOnLine.Areas.Management.Models;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Model.Resources;

namespace StoreOnLine.Areas.Management.Controllers
{
    [Authorize]
    public class AdminProductBaseController : Controller
    {
        private readonly IProductsRepository _repository;
        private readonly ICategoryRepository _repositoryCategory;
        private readonly ICampaingRepository _repositoryCampaing;

        public AdminProductBaseController(IProductsRepository repo, ICategoryRepository repositoryCategory, ICampaingRepository repositoryCampaing)
        {
            ViewBag.Big = "Administrar: Productos Base";
            ViewBag.Small = "Configuracion de Categorias de Productos Base";
            ViewBag.Area = "Management";
            ViewBag.Controller = "AdminProductBase";
            ViewBag.Action = "Index";
            _repository = repo;
            _repositoryCategory = repositoryCategory;
            _repositoryCampaing = repositoryCampaing;
        }

        public ViewResult Index()
        {
            return View(_repository.ProductBases);
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
                    //product.ProductImagens.Add(imagen);

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