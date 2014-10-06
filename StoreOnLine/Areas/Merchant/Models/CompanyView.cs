using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using StoreOnLine.DataBase.Model.Companies;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.Areas.Merchant.Models
{
    public class CompanyView
    {
        public CompanyView()
        {
            IsStatus = true;
        }
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Activo")]
        public bool IsStatus { get; set; }

        [Required(ErrorMessage = "Ingrese un Nombre para la compañia")]
        [DataType(DataType.Text)]
        [Display(Name = "Compañia")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Ingrese una actividad economica")]
        [DataType(DataType.Text)]
        [Display(Name = "Act Econóico")]
        public string CompanyActivity { get; set; }

        [Required(ErrorMessage = "Ingrese codigo de C.I.F.")]
        [DataType(DataType.Text)]
        [Display(Name = "C.I.F.")]
        public string CompanyCif { get; set; }

        [Required(ErrorMessage = "Ingrese el Numero de Seguro")]
        [Display(Name = "Seguro Soc")]
        public string CompanySecurityNumber { get; set; }

        [Required(ErrorMessage = "Ingrese el Numero de Seguro")]
        [Display(Name = "Ruc")]
        public string CompanyDocumentRuc { get; set; }

        public int CompanyAddressId { get; set; }

        [Required(ErrorMessage = "Ingrese su direccion")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Direccion 1")]
        public string CompanyAddressLine1 { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Direccion 2")]
        public string CompanyAddressLine2 { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Referencia")]
        public string CompanyAddressReference { get; set; }

        public int CompanyAddressUbigeoId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Departamento")]
        public string CompanyAddressUbigeoCodDpto { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Provincia")]
        public string CompanyAddressUbigeoCodProv { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Distrito")]
        public string CompanyAddressUbigeoCodDist { get; set; }

        public string CompanyAddressUbigeoNameUbiGeo { get; set; }

        public int CompanyContactNumberId { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefono")]
        public string CompanyContactNumberNumberPhone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Celular")]
        public string CompanyContactNumberCellPhone { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E Mail")]
        public string CompanyContactNumberEmail { get; set; }
        public bool CompanyContactNumberIsPrincipal { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public int CompanyPersonId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Estado")]
        public string EstadoTarea { get; set; }

        public Company ToBd(CompanyView view)
        {
            var address = new Address(CompanyAddressId, CompanyAddressLine1, CompanyAddressLine2, CompanyAddressReference, CompanyAddressUbigeoId);
            var number = new ContactNumber(CompanyContactNumberId, CompanyContactNumberNumberPhone, CompanyContactNumberCellPhone, CompanyContactNumberEmail);
            return new Company
            {
                Id = view.Id,
                StatusOfSchedule= view.EstadoTarea,
                IsStatus = view.IsStatus,
                CompanyName = view.CompanyName,
                CompanyActivity = view.CompanyActivity,
                CompanyCif = view.CompanyCif,
                CompanySecurityNumber = view.CompanySecurityNumber.ToString(CultureInfo.InvariantCulture),
                AddressId = view.CompanyAddressId,
                Address = address,
                ContactNumberId = view.CompanyContactNumberId,
                ContactNumber = number,
                PersonId = view.CompanyPersonId,
                CompanyDocumentRuc = CompanyDocumentRuc,
                CompanyCode=DateTime.Now.Millisecond.ToString(CultureInfo.InvariantCulture)
            };
        }

        public CompanyView ToView(Company db)
        {
            return new CompanyView
            {
                Id = db.Id,
                EstadoTarea=db.StatusOfSchedule,
                IsStatus = db.IsStatus,
                CompanyName = db.CompanyName,
                CompanyActivity = db.CompanyActivity,
                CompanyCif = db.CompanyCif,
                CompanySecurityNumber = db.CompanySecurityNumber,
                CompanyDocumentRuc = db.CompanyDocumentRuc,
                CompanyAddressId = db.Address.Id,
                CompanyAddressLine1 = db.Address.Line1,
                CompanyAddressLine2 = db.Address.Line2,
                CompanyAddressReference = db.Address.Reference,
                CompanyAddressUbigeoId = db.Address.UbigeoId,
                
                CompanyAddressUbigeoCodDpto = db.Address.Ubigeo.CodDpto,
                CompanyAddressUbigeoCodProv = db.Address.Ubigeo.CodProv,
                CompanyAddressUbigeoCodDist = db.Address.Ubigeo.CodDist,
                CompanyAddressUbigeoNameUbiGeo = db.Address.Ubigeo.NameUbiGeo,
                CompanyContactNumberId = db.ContactNumber.Id,
                CompanyContactNumberNumberPhone = db.ContactNumber.NumberPhone,
                CompanyContactNumberCellPhone = db.ContactNumber.CellPhone,
                CompanyContactNumberEmail = db.ContactNumber.Email,
                CompanyContactNumberIsPrincipal = db.ContactNumber.IsPrincipal,
                CompanyPersonId = db.PersonId



            };
        }
    }
}