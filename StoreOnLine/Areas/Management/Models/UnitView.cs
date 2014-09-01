using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Model.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StoreOnLine.Areas.Management.Models
{
    public class UnitView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Esta Activo?")]
        public bool IsStatus { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre para la unidad")]
        [StringLength(20, ErrorMessage = "El nombre debe ser mayor a 5 caracteres y menor a 20", MinimumLength = 5)]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public string UnitName { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre para la unidad")]
        [StringLength(2, ErrorMessage = "El valor debe ser mayor a 2 caracteres", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Codigo")]
        public string UnitCode { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripcion")]
        public string UnitDescription { get; set; }


        public Unit ToBd(UnitView view, Imagen imagen)
        {
            return new Unit
            {
                Id = view.Id,
                IsStatus = view.IsStatus,
                UnitName  = view.UnitName,
                UnitDescription = view.UnitDescription,
                UnitCode = view.UnitCode,
                UnitPhoto = imagen
            };
        }

        public UnitView ToView(Unit db)
        {
            return new UnitView
            {
                Id = db.Id,
                IsStatus = db.IsStatus,
                UnitName = db.UnitName,
                UnitDescription = db.UnitDescription,
                UnitCode   = db.UnitCode
            };
        }
    }
}