using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreOnLine.Areas.Merchant.Models;
using StoreOnLine.DataBase.Abstract;

namespace StoreOnLine.Areas.Merchant.Controllers
{
    public class BusinessController : Controller
    {
        private readonly ICompanyRepository _repositoryCompany;
        private readonly IUbigeoRepository _repositoryUbigeo;
        private readonly IPersonRepository _repositoryPerson;
        private readonly ISecurityRepository _repositorySecurity;

        public BusinessController(ICompanyRepository repositoryCompany, IUbigeoRepository repositoryUbigeo, IPersonRepository repositoryPerson, ISecurityRepository repositorySecurity)
        {
            ViewBag.Big = "Clientes: Compañia";
            ViewBag.Small = "Lista de Compañias registradas";
            ViewBag.Area = "Merchant";
            ViewBag.Controller = "Business";
            ViewBag.Action = "Index";
            _repositoryCompany = repositoryCompany;
            _repositoryUbigeo = repositoryUbigeo;
            _repositoryPerson = repositoryPerson;
            _repositorySecurity = repositorySecurity;
        }
        public ActionResult Index()
        {
            ViewBag.Action = "Index";
            var db = _repositoryCompany.Companies.Where(p => p.CompanyType == CompanyType.External.ToString());
            return View(db.Select(company => new CompanyView().ToView(company)).ToList());
        }

        //
        // GET: /Merchant/Bussines/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Merchant/Bussines/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Merchant/Bussines/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Merchant/Bussines/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Merchant/Bussines/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Merchant/Bussines/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Merchant/Bussines/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
