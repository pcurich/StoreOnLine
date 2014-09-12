using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Model.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StoreOnLine.Areas.Management.Models
{
    public class ProductBaseView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Esta Activo?")]
        public bool IsStatus { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre para el Producto Base")]
        [StringLength(20, ErrorMessage = "El nombre debe ser mayor a 5 caracteres y menor a 30", MinimumLength = 5)]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public String ProductName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripcion")]
        public String ProductDescription { get; set; }

        [Required]
        [Range(0.00, 10000, ErrorMessage = "Ingrese un valor positivo menor a 10000")]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio Base")]
        public Decimal ProductBasePrice { get; set; }

        [Required]
        [Range(0.00, 10000, ErrorMessage = "Ingrese un valor positivo menor a 10000")]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio Venta")]
        public Decimal ProductSalePrice { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Seleccione una categoria")]
        [DataType(DataType.Currency)]
        public int ProductCategoryId { get; set; }

        [Display(Name = "Campaña")]
        [Required(ErrorMessage = "Seleccione una campaña")]
        public int ProductCampaignId { get; set; }

        [Display(Name = "Unidad")]
        [Required(ErrorMessage = "Seleccione una unidad de medida")]
        public int ProductUnitId { get; set; }

        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "Seleccione un proveedor")]
        public int ProductSupplierId { get; set; }

        [Display(Name = "Imagen")]
        public List<Imagen> ProductImagens { get; set; }

        public List<Feature> Details { get; set; }

        public ProductBase ToBd(ProductBaseView view, Imagen imagen)
        {
            return new ProductBase
            {
                Id = view.Id,
                IsStatus = view.IsStatus,
                ProductName = view.ProductName,
                ProductDescription = view.ProductDescription,
                ProductBasePrice = view.ProductBasePrice,
                ProductSalePrice = view.ProductSalePrice,
                ProductCategoryId = view.ProductCategoryId,
                ProductCampaignId = view.ProductCampaignId,
                ProductUnitId = view.ProductUnitId,
                ProductSupplierId = view.ProductSupplierId,
                ProductImagens = view.ProductImagens
            };
        }

        public ProductBaseView ToView(ProductBase db)
        {
            return new ProductBaseView
            {
                Id = db.Id,
                IsStatus = db.IsStatus,
                ProductName = db.ProductName,
                ProductDescription = db.ProductDescription,
                ProductBasePrice = db.ProductBasePrice,
                ProductSalePrice = db.ProductSalePrice,
                ProductCategoryId = db.ProductCategoryId,
                ProductCampaignId = db.ProductCampaignId,
                ProductUnitId = db.ProductUnitId,
                ProductSupplierId = db.ProductSupplierId,
                ProductImagens = db.ProductImagens
            };
        }
    }
}