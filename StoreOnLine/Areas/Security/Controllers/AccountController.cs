﻿using System.Web.Mvc;
using StoreOnLine.Areas.Security.Models;
using StoreOnLine.Infrastructure.Abstract;

namespace StoreOnLine.Areas.Security.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAuthProvider _authProvider;

        public AccountController(IAuthProvider auth)
        {
            _authProvider = auth;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View();
            if (_authProvider.Authenticate(model.UserName, model.Password))
            {
                return Redirect(returnUrl ?? Url.Action("Index", "Admin", new { Area = "Management" }));
            }

            ModelState.AddModelError("", "Incorrect username or password");
            return View();
        }
    }
}