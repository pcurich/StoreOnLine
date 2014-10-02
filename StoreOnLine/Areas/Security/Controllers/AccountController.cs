using System.Web.Mvc;
using System.Web.Security;
using StoreOnLine.Areas.Security.Models;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.Security;
using StoreOnLine.Infrastructure.Abstract;
using StoreOnLine.Infrastructure.Security;

namespace StoreOnLine.Areas.Security.Controllers
{
    public class AccountController : Controller
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

        //[HttpPost]
        //public ActionResult Login(LoginViewModel model, string returnUrl)
        //{
        //    if (!ModelState.IsValid) return View();
        //    if (_authProvider.Authenticate(model.UserName, model.Password))
        //    {
        //        return Redirect(returnUrl ?? Url.Action("Index", "Admin", new { Area = "Management" }));
        //    }

        //    ModelState.AddModelError("", "Incorrect username or password");
        //    return View();
        //}


        [HttpPost]
        public ActionResult Login(User user, string username, string password, string returnUrl)
        {
            user.UserName = username;
            //bool result = FormsAuthentication.Authenticate(username, password);
            var userProvider = new StoreOnLIneMemberShipProvider(_repositorySecurity);
            var result = userProvider.ValidateUser(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);

                var rolProvider = new StoreOnLIneRoleProvider(_repositoryPerson);
                // return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                if (rolProvider.IsUserInRole(username, RoleList.Administrador.ToString()))
                {
                    return Redirect(Url.Action("Index", "Person", new { Area = "Security" }));
                }

                if (rolProvider.IsUserInRole(username, RoleList.Supervisor.ToString()))
                {
                    return Redirect(Url.Action("Index", "Business", new { Area = "Merchant" }));
                }

                if (rolProvider.IsUserInRole(username, RoleList.Empleado.ToString()))
                {
                    return Redirect(Url.Action("Index", "Admin", new { Area = "Report" }));
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