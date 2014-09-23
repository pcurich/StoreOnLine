using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.Areas.Security.Models
{
    public class PersonView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Esta Activo?")]
        public bool IsStatus { get; set; }

        [Required(ErrorMessage = "Ingrese el Nombre")]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Ingrese los Apellidos")]
        [DataType(DataType.Text)]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo Electronico")]
        public string Eamil { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Nume")]
        public string PhoneNumber { get; set; }

        public DocumentView Documents { get; set; }
        public ContactNumberView ContactNumbers { get; set; }
        public AddressView HomeAddress { get; set; }

        public UserView User { get; set; }
        public RoleView Role { get; set; }

        public PersonView()
        {
            Documents = new DocumentView();
            ContactNumbers = new ContactNumberView();
            HomeAddress = new AddressView();
            User = new UserView();
            Role = new RoleView();
            IsStatus = true;
        }

        public Person ToBd(PersonView view)
        {
            return new Person
            {
                Id = view.Id,
                IsStatus = view.IsStatus,
                FirstName = view.FirstName,
                LastName = view.LastName,
                BirthDate = view.BirthDate,
                // HomeAddress = view.HomeAddress,
                User = view.User.ToBd(view.User),
                Role = view.Role.ToBd(view.Role)

            };
        }

        public PersonView ToView(Person db)
        {
            return new PersonView
            {
                Id = db.Id,
                IsStatus = db.IsStatus,
                FirstName = db.FirstName,
                LastName = db.LastName,
                BirthDate = db.BirthDate,
                User = new UserView().ToView(db.User),
                Role = new RoleView().ToView(db.Role)
            };
        }
    }

    public class UserView
    {
        [Required(ErrorMessage = "Ingrese un Usuario")]
        [DataType(DataType.Text)]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Ingrese una contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string UserPassword { get; set; }

        public User ToBd(UserView view)
        {
            return new User
            {
                UserName = view.UserName,
                UserPassword = view.UserPassword
            };
        }

        public UserView ToView(User db)
        {
            return new UserView
            {
                UserPassword = db.UserPassword,
                UserName = db.UserName
            };
        }
    }

    public class RoleView
    {
        [Required(ErrorMessage = "Seleccione un Rol")]
        [DataType(DataType.Text)]
        [Display(Name = "Rol")]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Rol")]
        public string RoleName { get; set; }

        public Role ToBd(RoleView view)
        {
            return new Role
            {
                Id=view.Id,
                RoleName = view.RoleName
            };
        }

        public RoleView ToView(Role db)
        {
            return new RoleView
            {
                Id = db.Id,
                RoleName = db.RoleName
            };
        }
    }

    public class DocumentView
    {
        [DataType(DataType.Text)]
        [Display(Name = "Tipo Doc")]
        public string DocumentTypeName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Numero")]
        public string DocumentValue { get; set; }

        public Document ToBd(DocumentView view)
        {
            return new Document
            {
                DocumentTypeName = view.DocumentTypeName,
                DocumentValue = view.DocumentValue
            };
        }

        public DocumentView ToView(Document db)
        {
            return new DocumentView
            {
                DocumentTypeName = db.DocumentTypeName,
                DocumentValue = db.DocumentValue
            };
        }
    }

    public class ContactNumberView
    {
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Celular")]
        public string CellPhone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Numero")]
        public string NumberPhone { get; set; }

        [Display(Name = "principal")]
        public bool IsPrincipal { get; set; }

        public ContactNumber ToBd(ContactNumberView view)
        {
            return new ContactNumber
            {
                NumberPhone = view.NumberPhone,
                CellPhone=view.CellPhone,
                IsPrincipal = view.IsPrincipal
            };
        }

        public ContactNumberView ToView(ContactNumber db)
        {
            return new ContactNumberView
            {
                NumberPhone = db.NumberPhone,
                CellPhone = db.CellPhone,
                IsPrincipal = db.IsPrincipal
            };
        }
    }

    public class AddressView
    {
        [DataType(DataType.Text)]
        [Display(Name = "Direccion1")]
        public string Line1 { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Direccion2")]
        public string Line2 { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Referencia")]
        public string Reference { get; set; }

        [Display(Name = "Direccion Actual")]
        public bool IsPrincipal { get; set; }

        public UbigeoView Ubigeo { get; set; }

        public Address ToBd(AddressView view)
        {
            return new Address
            {
                Line1 = view.Line1,
                Line2 = view.Line2,
                Ubigeo = view.Ubigeo.ToBd(view.Ubigeo),
                Reference  =view.Reference,
                IsPrincipal = view.IsPrincipal
            };
        }

        public AddressView ToView(Address db)
        {
            return new AddressView
            {
                Line1 = db.Line1,
                Line2 = db.Line2,
                Ubigeo = new UbigeoView().ToView(db.Ubigeo),
                IsPrincipal = db.IsPrincipal,
                Reference = db.Reference
            };
        }
    }

    public class UbigeoView
    {
        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "Seleccione un Departamento")]
        public string CodDpto { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "Seleccione una Provincia")]
        public string CodProv { get; set; }

        [Display(Name = "Distrito")]
        [Required(ErrorMessage = "Seleccione un Distrito")]
        public string CodDist { get; set; }

        public string NameUbiGeo { get; set; }

        public UbigeoView()
        {
            CodDpto = "0";
            CodProv = "0";
            CodDist = "0";
            NameUbiGeo = "";
        }

        public Ubigeo ToBd(UbigeoView view)
        {
            return new Ubigeo
            {
                CodDpto = view.CodDpto,
                CodProv = view.CodProv,
                CodDist = view.CodDist,
                NameUbiGeo = view.NameUbiGeo,
            };
        }

        public UbigeoView ToView(Ubigeo db)
        {
            return new UbigeoView
            {
                CodDpto = db.CodDpto,
                CodProv = db.CodProv,
                CodDist = db.CodDist,
                NameUbiGeo = db.NameUbiGeo,
            };
        }
    }
}