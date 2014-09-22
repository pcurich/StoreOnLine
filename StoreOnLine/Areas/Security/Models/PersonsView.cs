using System;
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

        public Address HomeAddress { get; set; }
        public UserView User { get; set; }
        public Role Role { get; set; }

        public PersonView()
        {
            HomeAddress=new Address();
            User = new UserView();
            Role=new Role();
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
                HomeAddress = view.HomeAddress,
                User = view.User.ToBd(view.User),
                Role = view.Role

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
                HomeAddress = db.HomeAddress,
                User = new UserView().ToView(db.User),
                Role = db.Role
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
}