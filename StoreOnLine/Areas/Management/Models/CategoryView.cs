using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Model.Resources;

namespace StoreOnLine.Areas.Management.Models
{
    public class CategoryView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Esta Activo?")]
        public bool IsStatus { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre para la categoria")]
        [StringLength(20, ErrorMessage = "El nombre debe ser mayor a 5 caracteres y menor a 20", MinimumLength = 5)]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public string CategoryName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripcion")]
        public string CategoryDescription { get; set; }


        public Category ToBd(CategoryView view,Imagen imagen)
        {
            return new Category
            {
                Id = view.Id,
                IsStatus = view.IsStatus,
                CategoryName = view.CategoryName,
                CategoryDescription = view.CategoryDescription,
                CategoryPhoto= imagen
            };
        }

        public CategoryView ToView(Category db)
        {
            return new CategoryView
            {
                Id = db.Id,
                IsStatus = db.IsStatus,
                CategoryName = db.CategoryName,
                CategoryDescription = db.CategoryDescription
            };
        }

    }
}