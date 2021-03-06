﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.Areas.Security.Models
{
    public class PersonView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Activo")]
        public bool IsStatus { get; set; }

        [Required(ErrorMessage = "Ingrese el Nombre")]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Ingrese los Apellidos")]
        [DataType(DataType.Text)]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Ingrese una fecha de nacimiento")]
        [DataType(DataType.Text)]
        [Display(Name = "Fecha de Nacimiento")]
        public string BirthDate { get; set; }

        public string DocumentId { get; set; }

        [StringLength(8,ErrorMessage = "El dni posee 8 digitos",MinimumLength = 8)]
        [Required(ErrorMessage = "Ingrese el numero de dni")]
        [DataType(DataType.Text)]
        [Display(Name = "DNI")]
        public string DocumentDni { get; set; }

        [StringLength(11, ErrorMessage = "El ruc posee 11 digitos", MinimumLength = 11)]
        [DataType(DataType.Text)]
        [Display(Name = "RUC")]
        public string DocumentRuc { get; set; }

        public string ContactNumberId { get; set; }

        [StringLength(7, ErrorMessage = "El Numero de telefono debe ser de 7 digitos ")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telf Fijo")]
        public string NumberPhone { get; set; }

        [StringLength(9, ErrorMessage = "El Numero de celular debe ser de 9 digitos ")]
        [Required(ErrorMessage = "Ingrese un numero de celular")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Num Celular")]
        public string CellPhone { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public string AddressId { get; set; }

        [Required(ErrorMessage = "Ingrese al menos una direccion")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Direccion 1")]
        public string Line1 { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Direccion 2")]
        public string Line2 { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Referencia")]
        public string Reference { get; set; }

        public string UbigeoId { get; set; }

        [Required(ErrorMessage = "Seleccione un departamento")]
        [DataType(DataType.Text)]
        [Display(Name = "Provincia")]
        public string CodDpto { get; set; }

        [Required(ErrorMessage = "Seleccione una provincia")]
        [DataType(DataType.Text)]
        [Display(Name = "Provincia")]
        public string CodProv { get; set; }

        [Required(ErrorMessage = "Seleccione un distrito")]
        [DataType(DataType.Text)]
        [Display(Name = "Distrito")]
        public string CodDist { get; set; }

        public string UserId { get; set; }

        [StringLength(5,ErrorMessage = "El numero Interno debe tener 5 digitos",MinimumLength = 5)]
        [Required(ErrorMessage = "Ingrese un codigo de usuario")]
        [DataType(DataType.Text)]
        [Display(Name = "NI")]
        public string UserCode { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre de usuario")]
        [DataType(DataType.Text)]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Ingrese una contraseña de usuario")]
        [DataType(DataType.Password)]
        [Display(Name = "Usuario")]
        public string UserPassword { get; set; }

        public string RoleId { get; set; }

        [Required(ErrorMessage = "Seleccione un rol")]
        [DataType(DataType.Text)]
        [Display(Name = "Rol")]
        public string RoleCode { get; set; }
        public string RoleName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Base")]
        public string BaseCode { get; set; }

        public Person ToBd(PersonView view)
        {
            var dateStr = view.BirthDate.Split('/');
            var date = new DateTime(Convert.ToInt16(dateStr[2]), Convert.ToInt16(dateStr[1]), Convert.ToInt16(dateStr[0]));
            var document = new Document(Convert.ToInt16(view.DocumentId), view.DocumentDni,  view.DocumentRuc);
            var contactNumber = new ContactNumber(Convert.ToInt16(view.ContactNumberId), view.NumberPhone, view.CellPhone, view.Email);
            var address = new Address(Convert.ToInt16(view.AddressId), view.Line1, view.Line2, view.Reference, Convert.ToInt16(view.UbigeoId));
            var user = new User(Convert.ToInt16(view.UserId), view.UserCode, view.UserName, view.UserPassword);

            return new Person
            {
                Id = view.Id,
                Active = view.IsStatus,
                FirstName = view.FirstName,
                LastName = view.LastName,
                BirthDate = date,
                DocumentId = Convert.ToInt16(view.DocumentId),
                Document = document,
                ContactNumberId = Convert.ToInt16(view.ContactNumberId),
                ContactNumber = contactNumber,
                UserId = Convert.ToInt16(view.UserId),
                User = user,
                RoleId = Convert.ToInt16(view.RoleId),
                AddressId = Convert.ToInt16(view.AddressId),
                Address = address,
                BaseCode = view.BaseCode,
            };
        }

        public PersonView ToView(Person db)
        {
            return new PersonView
            {
                Id = db.Id,
                IsStatus = db.Active,
                FirstName = db.FirstName,
                LastName = db.LastName,
                BirthDate = db.BirthDate.ToShortDateString(),
                DocumentId = db.DocumentId.ToString(CultureInfo.InvariantCulture),
                DocumentRuc = db.Document.DocumentRuc,
                DocumentDni = db.Document.DocumentDni,
                ContactNumberId = db.ContactNumberId.ToString(CultureInfo.InvariantCulture),
                NumberPhone = db.ContactNumber.NumberPhone,
                CellPhone = db.ContactNumber.CellPhone,
                Email = db.ContactNumber.Email,
                AddressId = db.AddressId.ToString(CultureInfo.InvariantCulture),
                Line1 = db.Address.Line1,
                Line2 = db.Address.Line2,
                Reference = db.Address.Reference,
                UbigeoId = db.Address.UbigeoId.ToString(CultureInfo.InvariantCulture),
                CodDpto = db.Address.Ubigeo.CodDpto,
                CodProv = db.Address.Ubigeo.CodProv,
                CodDist = db.Address.Ubigeo.CodDist,
                UserId = db.UserId.ToString(CultureInfo.InvariantCulture),
                UserCode = db.User.UserCode,
                UserName = db.User.UserName,
                UserPassword = db.User.UserPassword,
                RoleId = db.RoleId.ToString(CultureInfo.InvariantCulture),
                RoleCode = db.Role.RoleCode,
                RoleName = db.Role.RoleName,
                BaseCode = db.BaseCode,
            };
        }
    }
}