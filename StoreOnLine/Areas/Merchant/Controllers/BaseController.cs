using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using StoreOnLine.Areas.Merchant.Models;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.Areas.Merchant.Controllers
{
    public class BaseController : Controller
    {
        private readonly ICompanyRepository _repositoryCompany;
        private readonly IUbigeoRepository _repositoryUbigeo;
        private readonly IPersonRepository _repositoryPerson;
        private readonly ISecurityRepository _repositorySecurity;

        public BaseController(ICompanyRepository repositoryCompany, IUbigeoRepository repositoryUbigeo, IPersonRepository repositoryPerson, ISecurityRepository repositorySecurity)
        {
            ViewBag.Big = "Locales: Base de operaciones";
            ViewBag.Small = "Lista de bases registradas";
            ViewBag.Area = "Merchant";
            ViewBag.Controller = "Base";
            ViewBag.Action = "Index";
            _repositoryCompany = repositoryCompany;
            _repositoryUbigeo = repositoryUbigeo;
            _repositoryPerson = repositoryPerson;
            _repositorySecurity = repositorySecurity;
        }

        public ActionResult Index()
        {
            ViewBag.Action = "Index";
            var db = _repositoryCompany.Companies.Where(p => p.CompanyType == CompanyType.Internal.ToString());
            return View(db.Select(company => new CompanyView().ToView(company)).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.Action = "Create";
            ViewBag.Dpto = GetDeparts(null);
            ViewBag.Prov = new SelectList(new List<SelectListItem>());
            ViewBag.Dist = new SelectList(new List<SelectListItem>());
            var role = _repositorySecurity.Roles.FirstOrDefault(o => o.RoleName == RoleList.Supervisor.ToString());
            ViewBag.Person = GetPerson(null, role != null ? role.Id : 0);
            return View("Edit", new CompanyView());
        }

        public ActionResult Edit(int companyId)
        {
            ViewBag.Action = "Edit";
            var company = _repositoryCompany.Companies.Where(p => p.CompanyType == CompanyType.Internal.ToString()).FirstOrDefault(p => p.Id == companyId);
            if (company != null)
            {
                ViewBag.CompanyName = company.CompanyName;
                ViewBag.Dpto = GetDeparts(company.Address.Ubigeo.CodDpto);
                ViewBag.Prov = GetProvincesList(company.Address.Ubigeo.CodDpto, company.Address.Ubigeo.CodProv);
                ViewBag.Dist = GetDistrictsList(company.Address.Ubigeo.CodDpto, company.Address.Ubigeo.CodProv, company.Address.Ubigeo.CodDist);
                var role = _repositorySecurity.Roles.FirstOrDefault(o => o.RoleName == RoleList.Supervisor.ToString());
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

                model.EstadoTarea = StatusOfSchedule.NoIniciada.ToString();

                var db = model.ToBd(model, CompanyType.Internal.ToString());
                db.CompanyType = CompanyType.Internal.ToString();


                db.AddressId = _repositoryPerson.SaveAddress(db.Address);
                db.Address = null;
                db.ContactNumberId = _repositoryPerson.SaveContactNumber(db.ContactNumber);
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