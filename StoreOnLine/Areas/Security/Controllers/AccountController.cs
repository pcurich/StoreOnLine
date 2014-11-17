﻿using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using StoreOnLine.Areas.Security.Models;
using StoreOnLine.Controllers;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.CmsEmploye;
using StoreOnLine.DataBase.Model.Security;
using StoreOnLine.Infrastructure.Abstract;
using StoreOnLine.Infrastructure.Security;
using StoreOnLine.Service.Constants;
using StoreOnLine.Service.Service.Employers;

namespace StoreOnLine.Areas.Security.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAuthProvider _authProvider;
        private readonly ISecurityRepository _repositorySecurity;
        private readonly IPersonRepository _repositoryPerson;

        public AccountController(IAuthProvider auth, ISecurityRepository repositorySecurity, IPersonRepository repositoryPerson)
        {
            _authProvider = auth;
            _repositorySecurity = repositorySecurity;
            _repositoryPerson = repositoryPerson;
        }

        public ViewResult Login()
        {
            return View();
        }

 
        [HttpPost]
        public ActionResult Login(ViewEmployer viewEmployer, string username, string password, string returnUrl)
        {
            //return Redirect(Url.Action("Index", "Category", new { Area = "Catalog" }));
            viewEmployer.UserName = username;
            //bool result = FormsAuthentication.Authenticate(username, password);
            var userProvider = new StoreOnLIneMemberShipProvider();
            var result = userProvider.ValidateUser(username, password);

            Session[Enums.EmployerOnLine] = ServEmployer.GetCurrentViewEmployer();// userProvider.GetEmployer();

            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);

                var rolProvider = new StoreOnLIneRoleProvider();
                // return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                if (rolProvider.IsUserInRole(username, Enums.RolEnSuperAdmin))
                {
                    return Redirect(Url.Action("Index", "Category", new { Area = "Catalog" }));
                    //return Redirect(Url.Action("Index", "Person", new { Area = "Security" }));
                }

                if (rolProvider.IsUserInRole(username, RoleList.Supervisor.ToString()))
                {
                    return Redirect(Url.Action("Index", "Business", new { Area = "Merchant" }));
                }

                if (rolProvider.IsUserInRole(username, RoleList.Empleado.ToString()))
                {
                    return Redirect(Url.Action("Index", "Admin", new { Area = "Report" }));

                }

                if (rolProvider.IsUserInRole(username, RoleList.Representante.ToString()))
                {
                    ModelState.AddModelError("", "No tiene permisos de acceso al sistema");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect username or password");
                return View();
            }
            return View();
        }

        public ActionResult Logout(User user)
        {
            user.UserName = null;
            FormsAuthentication.SignOut();
            FormsAuthentication.SetAuthCookie(null, false);
            return RedirectToAction("LogIn","Account",new {Area="Security"});
        }
    }
}