using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreOnLine.Areas.Security.Models;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.Areas.Security.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _repositoryPerson;
        private readonly IUbigeoRepository _repositoryUbigeo;
        private readonly ISecurityRepository _repositorySecurity;

        public PersonController(IPersonRepository repoPerson, IUbigeoRepository repoUbigeo, ISecurityRepository repoSecurity)
        {
            ViewBag.Big = "Administrar: Usuarios";
            ViewBag.Small = "Lista de Usuarios";
            ViewBag.Area = "Security";
            ViewBag.Controller = "Person";
            ViewBag.Action = "Index";
            _repositoryPerson = repoPerson;
            _repositoryUbigeo = repoUbigeo;
            _repositorySecurity = repoSecurity;
        }

        //
        // GET: /Security/User/
        public ActionResult Index()
        {
            var db = _repositoryPerson.Persons;
            var view = db.Select(person => new PersonView().ToView(person)).ToList();
            return View(view);
        }

        public ViewResult Edit(int personId)
        {
            ViewBag.Action = "Edit";

            var person = _repositoryPerson.Persons.FirstOrDefault(p => p.Id == personId);
            if (person != null) ViewBag.UserName = person.User.UserName;

            if (person != null)
            {
                ViewBag.Dpto = GetDeparts(person.HomeAddress.Ubigeo.CodDpto);
                ViewBag.Prov = GetProvincesList(person.HomeAddress.Ubigeo.CodDpto, person.HomeAddress.Ubigeo.CodProv);
                ViewBag.Dist = GetDistrictsList(person.HomeAddress.Ubigeo.CodDpto, person.HomeAddress.Ubigeo.CodProv, person.HomeAddress.Ubigeo.CodDist);
                ViewBag.Role = GetRolesList(person.Role.Id.ToString(CultureInfo.InvariantCulture));
            }
            return View(new PersonView().ToView(person));
        }

        [HttpPost]
        public ActionResult Edit(PersonView model)
        {
            ViewBag.Action = "Edit";
            if (ModelState.IsValid)
            {
                var exist = _repositoryPerson.Persons.FirstOrDefault(o => o.Id == model.Id);
                if (exist != null && exist.Id != 0)
                {
                    model.HomeAddress.Id = model.HomeAddress.Id == 0 ? exist.HomeAddress.Id : model.HomeAddress.Id;
                    model.User.Id = model.User.Id == 0 ? exist.User.Id : model.User.Id;
                    model.Role.Id = model.Role.Id == 0 ? exist.Role.Id : model.Role.Id;
                    model.ContactNumber.Id = model.ContactNumber.Id == 0 ? exist.ContactNumber.Id : model.ContactNumber.Id;
                    model.Document.Id = model.Document.Id == 0 ? exist.Document.Id : model.Document.Id;
                }
                _repositoryPerson.SavePerson(model.ToBd(model));
                TempData["message"] = string.Format("{0} ha sido guardado", model.User.UserName);
                return Json(new { ok = true, newurl = "Index" });
            }
            ViewBag.Dpto = GetDeparts(model.HomeAddress.Ubigeo.CodDpto);
            ViewBag.Prov = ViewBag.Prov = GetProvincesList(model.HomeAddress.Ubigeo.CodDpto, model.HomeAddress.Ubigeo.CodProv);
            ViewBag.Dist = GetDistrictsList(model.HomeAddress.Ubigeo.CodDpto, model.HomeAddress.Ubigeo.CodProv, model.HomeAddress.Ubigeo.CodDist);
            ViewBag.Role = GetRolesList(model.Role.CodRole);
            // there is something wrong with the data values
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.Action = "Create";
            ViewBag.Dpto = GetDeparts(null);
            ViewBag.Role = GetRolesList(null);
            ViewBag.Prov = new SelectList(new List<SelectListItem>());
            ViewBag.Dist = new SelectList(new List<SelectListItem>());
            return View("Edit", new PersonView());
        }


        public ActionResult Delete(int personId)
        {
            var deletedPerson = _repositoryPerson.DeletePerson(personId);
            if (deletedPerson != null)
            {
                TempData["message"] = string.Format("{0} fue eliminado", deletedPerson.User.UserName);
            }
            return RedirectToAction("Index");
        }

        #region Custom

        public SelectList GetDeparts(string selected)
        {
            return _repositoryUbigeo.GetDepart(selected);
        }

        public SelectList GetProvincesList(string codDpto, string selected)
        {
            return _repositoryUbigeo.GetProvince(codDpto, selected);
        }

        public SelectList GetDistrictsList(string codDpto, string codProv, string selected)
        {
            return _repositoryUbigeo.GetDistrict(codDpto, codProv, selected);
        }

        public JsonResult GetProvinces(string codDpto, string selected)
        {
            return Json(_repositoryUbigeo.GetProvince(codDpto, selected));
        }

        public JsonResult GetDistricts(string codDpto, string codProv, string selected)
        {
            return Json(_repositoryUbigeo.GetDistrict(codDpto, codProv, selected));
        }

        public SelectList GetRolesList(string selected)
        {
            return _repositorySecurity.GetRoles(selected);
        }

        public SelectList GetUserList(string selected)
        {
            return _repositorySecurity.GetUser(selected);
        }

        #endregion
    }
}