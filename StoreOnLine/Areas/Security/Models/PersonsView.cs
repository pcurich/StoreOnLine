using StoreOnLine.DataBase.Model.Security;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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
        [DataType(DataType.Text)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime BirthDate { get; set; }

        public DocumentView Document { get; set; }
        public ContactNumberView ContactNumber { get; set; }
        public AddressView HomeAddress { get; set; }

        public UserView User { get; set; }
        public RoleView Role { get; set; }

        public PersonView()
        {
            BirthDate = new DateTime(1900, 1, 1);
            Document = new DocumentView();
            ContactNumber = new ContactNumberView();
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
                ContactNumber = view.ContactNumber.ToBd(view.ContactNumber),
                HomeAddress = view.HomeAddress.ToBd(view.HomeAddress),
                Document = view.Document.ToBd(view.Document),
                User = view.User.ToBd(view.User),
                UserId = view.User.ToBd(view.User).Id,
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
                Document = new DocumentView().ToView(db.Document),
                ContactNumber = new ContactNumberView().ToView(db.ContactNumber),
                HomeAddress = new AddressView().ToView(db.HomeAddress),
                User = new UserView().ToView(db.User),
                Role = new RoleView().ToView(db.Role)
            };
        }
    }

    public class UserView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese un codigo")]
        [DataType(DataType.Text)]
        [Display(Name = "Codigo")]
        public string CodUser { get; set; }

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
                Id = view.Id,
                CodUser = view.CodUser,
                UserName = view.UserName,
                UserPassword = view.UserPassword
            };
        }

        public UserView ToView(User db)
        {
            return new UserView
            {
                Id = db.Id,
                CodUser = db.CodUser,
                UserPassword = db.UserPassword,
                UserName = db.UserName
            };
        }
    }

    public class RoleView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Codigo")]
        public string CodRole { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public string RoleName { get; set; }

        public Role ToBd(RoleView view)
        {
            return new Role
            {
                Id = view.Id,
                CodRole = view.CodRole,
                RoleName = view.RoleName
            };
        }

        public RoleView ToView(Role db)
        {
            return new RoleView
            {
                Id = db.Id,
                CodRole = db.CodRole,
                RoleName = db.RoleName
            };
        }
    }

    public class DocumentView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

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
                Id = view.Id,
                DocumentTypeName = view.DocumentTypeName,
                DocumentValue = view.DocumentValue
            };
        }

        public DocumentView ToView(Document db)
        {
            return new DocumentView
            {
                Id = db.Id,
                DocumentTypeName = db.DocumentTypeName,
                DocumentValue = db.DocumentValue
            };
        }
    }

    public class ContactNumberView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Celular")]
        public string CellPhone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Numero")]
        public string NumberPhone { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }

        [Display(Name = "principal")]
        public bool IsPrincipal { get; set; }

        public ContactNumber ToBd(ContactNumberView view)
        {
            return new ContactNumber
            {
                Id = view.Id,
                NumberPhone = view.NumberPhone,
                CellPhone = view.CellPhone,
                Email = view.Email,
                IsPrincipal = view.IsPrincipal
            };
        }

        public ContactNumberView ToView(ContactNumber db)
        {
            return new ContactNumberView
            {
                Id = db.Id,
                NumberPhone = db.NumberPhone,
                CellPhone = db.CellPhone,
                Email = db.Email,
                IsPrincipal = db.IsPrincipal
            };
        }
    }

    public class AddressView
    {
        public AddressView()
        {
            Ubigeo = new UbigeoView();
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

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
                Id = view.Id,
                Line1 = view.Line1,
                Line2 = view.Line2,
                Ubigeo = view.Ubigeo.ToBd(view.Ubigeo),
                Reference = view.Reference,
                IsPrincipal = view.IsPrincipal
            };
        }

        public AddressView ToView(Address db)
        {
            return new AddressView
            {
                Id = db.Id,
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
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

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
                Id = view.Id,
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
                Id = db.Id,
                CodDpto = db.CodDpto,
                CodProv = db.CodProv,
                CodDist = db.CodDist,
                NameUbiGeo = db.NameUbiGeo,
            };
        }
    }
}