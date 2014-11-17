using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Model.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StoreOnLine.Areas.Management.Models
{
    public class ProductCompositeView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Esta Activo?")]
        public bool IsStatus { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre para el producto")]
        [StringLength(20, ErrorMessage = "El nombre debe ser mayor a 5 caracteres y menor a 20", MinimumLength = 5)]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public string ProductName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripcion")]
        public string ProductDescription { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Precio Base")]
        public Decimal ProductBasePrice { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Precio Venta")]
        public Decimal ProductSalePrice { get; set; }

        [Display(Name = "Categoria")]
        public int ProductCategoryId { get; set; }

        [Display(Name = "Campaña")]
        public int ProductCampaignId { get; set; }

        [Display(Name = "Unidad")]
        public int ProductUnitId { get; set; }


        public List<Imagen> ProductImagens { get; set; }

        public List<Feature> Features { get; set; }

        public ProductComposite ToBd(ProductCompositeView view)
        {
            return new ProductComposite
            {
                Id = view.Id,
                Active = view.IsStatus,
                ProductName = view.ProductName,
                ProductDescription = view.ProductDescription,
                ProductBasePrice = view.ProductBasePrice,
                ProductSalePrice = view.ProductSalePrice,
                ProductCategoryId = view.ProductCategoryId,
                ProductCampaignId = view.ProductCampaignId,
                ProductUnitId = view.ProductUnitId,
                ProductImagens = view.ProductImagens,
                Features = view.Features
            };
        }

        public ProductCompositeView ToView(ProductComposite db)
        {
            return new ProductCompositeView
            {
                Id = db.Id,
                IsStatus = db.Active,
                ProductName = db.ProductName,
                ProductDescription = db.ProductDescription,
                ProductBasePrice = db.ProductBasePrice,
                ProductSalePrice = db.ProductSalePrice,
                ProductCategoryId = db.ProductCategory.Id,
                ProductCampaignId = db.ProductCampaign.Id,
                ProductUnitId = db.ProductUnit.Id,
                ProductImagens = db.ProductImagens,
                Features = db.Features
            };
        }
    }
}