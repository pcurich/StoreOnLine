﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Configuration;
using System.Web.Mvc;
using StoreOnLine.Areas.Merchant.Models;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.Companies;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.Areas.Merchant.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _repositoryCompany;
        private readonly IUbigeoRepository _repositoryUbigeo;
        private readonly IPersonRepository _repositoryPerson;
        private readonly ISecurityRepository _repositorySecurity;

        public CompanyController(ICompanyRepository repositoryCompany, IUbigeoRepository repositoryUbigeo, IPersonRepository repositoryPerson, ISecurityRepository repositorySecurity)
        {
            ViewBag.Big = "Clientes: Compañia";
            ViewBag.Small = "Lista de Compañias registradas";
            ViewBag.Area = "Merchant";
            ViewBag.Controller = "Company";
            ViewBag.Action = "Index";
            _repositoryCompany = repositoryCompany;
            _repositoryUbigeo = repositoryUbigeo;
            _repositoryPerson = repositoryPerson;
            _repositorySecurity = repositorySecurity;
        }

        //
        // GET: /Merchant/Company/
        public ActionResult Index()
        {
            ViewBag.Action = "Index";
            var db = _repositoryCompany.Companies.Where(p => p.CompanyType == CompanyType.External.ToString());
            return View(db.Select(company => new CompanyView().ToView(company)).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.Action = "Create";
            ViewBag.Dpto = GetDeparts(null);
            ViewBag.Prov = new SelectList(new List<SelectListItem>());
            ViewBag.Dist = new SelectList(new List<SelectListItem>());
            var role = _repositorySecurity.Roles.FirstOrDefault(o => o.RoleName == RoleList.Representante.ToString());
            ViewBag.Person = GetPerson(null, role != null ? role.Id : 0);
            return View("Edit", new CompanyView());
        }

        public ActionResult Edit(int companyId)
        {
            ViewBag.Action = "Edit";
            var company = _repositoryCompany.Companies.Where(p => p.CompanyType == CompanyType.External.ToString()).FirstOrDefault(p => p.Id == companyId);
            if (company != null)
            {
                ViewBag.CompanyName = company.CompanyName;
                ViewBag.Dpto = GetDeparts(company.Address.Ubigeo.CodDpto);
                ViewBag.Prov = GetProvincesList(company.Address.Ubigeo.CodDpto, company.Address.Ubigeo.CodProv);
                ViewBag.Dist = GetDistrictsList(company.Address.Ubigeo.CodDpto, company.Address.Ubigeo.CodProv, company.Address.Ubigeo.CodDist);
                var role = _repositorySecurity.Roles.FirstOrDefault(o => o.RoleName == RoleList.Representante.ToString());
                var person = _repositoryPerson.Persons.FirstOrDefault(o => o.Id == company.PersonId);
                ViewBag.Person = GetPerson(person != null ? person.Id.ToString(CultureInfo.InvariantCulture) : null, role != null ? role.Id : 0);
            }

            return View(new CompanyView().ToView(company));
        }

        [HttpPost]
        public ActionResult Edit(CompanyView model)
        {
            ViewBag.Action = "Edit";
            if (ModelState.IsValid)
            {
                var ubigeo = _repositoryUbigeo.Ubigeos.FirstOrDefault(o => o.CodDpto == model.CompanyAddressUbigeoCodDpto
                                && o.CodProv == model.CompanyAddressUbigeoCodProv
                                && o.CodDist == model.CompanyAddressUbigeoCodDist);
                if (ubigeo != null)
                    model.CompanyAddressUbigeoId = ubigeo.Id;

                var company = _repositoryCompany.Companies.FirstOrDefault(o => o.Id == model.Id);
                if (company != null)
                {
                    if (company.Schedules.Count == 0)
                    {
                        model.EstadoTarea = StatusOfSchedule.NoIniciada.ToString();
                    }
                }

                var db = model.ToBd(model, CompanyType.External.ToString());

                db.AddressId = _repositoryPerson.SaveAddress(db.Address);
                db.ContactNumberId = _repositoryPerson.SaveContactNumber(db.ContactNumber);
                db.Address = null;
                db.ContactNumber = null;
                _repositoryCompany.SaveCompany(db);

                TempData["message"] = string.Format("{0} ha sido guardado", model.CompanyName);
                return Json(new { ok = true, newurl = "Index" });
            }
            return Json(new { ok = false, model });
        }

        public ActionResult Delete(int companyId)
        {
            var deletedCompany = _repositoryCompany.DeleteCompany(companyId);
            if (deletedCompany != null)
            {
                TempData["message"] = string.Format("{0} fue eliminado", deletedCompany.CompanyName);
            }
            return RedirectToAction("Index");
        }

        #region Custom

        public SelectList GetDeparts(string selected)
        {
            return _repositoryUbigeo.GetDepart(selected);
        }

        public JsonResult GetProvinces(string codDpto, string selected)
        {
            return Json(_repositoryUbigeo.GetProvince(codDpto, selected));
        }

        public JsonResult GetDistricts(string codDpto, string codProv, string selected)
        {
            return Json(_repositoryUbigeo.GetDistrict(codDpto, codProv, selected));
        }

        public SelectList GetPerson(string selected, int roleId)
        {
            return _repositoryPerson.GetPersons(selected, roleId);
        }

        public SelectList GetProvincesList(string codDpto, string selected)
        {
            return _repositoryUbigeo.GetProvince(codDpto, selected);
        }

        public SelectList GetDistrictsList(string codDpto, string codProv, string selected)
        {
            return _repositoryUbigeo.GetDistrict(codDpto, codProv, selected);
        }

        #endregion
    }
}
