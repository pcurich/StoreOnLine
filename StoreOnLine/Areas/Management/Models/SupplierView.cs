using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using StoreOnLine.DataBase.Model.Providers;

namespace StoreOnLine.Areas.Management.Models
{
    public class SupplierView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Esta Activo?")]
        public bool IsStatus { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre para el proveedor")]
        [StringLength(20, ErrorMessage = "El nombre debe ser mayor a 5 caracteres y menor a 20", MinimumLength = 5)]
        [DataType(DataType.Text)]
        [Display(Name = "Razon Social")]
        public String SupplierName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripcion")]
        public String SupplierDescription { get; set; }

        [Required(ErrorMessage = "Ingrese el R.U.C del proveedor")]
        [DataType(DataType.Text)]
        [Display(Name = "R.U.C")]
        public String SupplierDocument { get; set; }

        public Supplier ToBd(SupplierView view)
        {
            return new Supplier
            {
                Id = view.Id,
                IsStatus = view.IsStatus,
                SupplierName = view.SupplierName,
                SupplierDescription = view.SupplierDescription,
                SupplierDocument = view.SupplierDocument
            };
        }

        public SupplierView ToView(Supplier db)
        {
            return new SupplierView
            {
                Id = db.Id,
                IsStatus = db.IsStatus,
                SupplierName = db.SupplierName,
                SupplierDescription = db.SupplierDescription,
                SupplierDocument = db.SupplierDocument
            };
        }
    }
}