using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using StoreOnLine.Areas.Security.Models;
using StoreOnLine.DataBase.Abstract;

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
            ViewBag.Action = "Index";
            var db = _repositoryPerson.Persons;
            var view = db.Select(person => new PersonView().ToView(person)).ToList();
            return View(view);
        }

        public ViewResult Edit(int personId)
        {
            ViewBag.Action = "Edit";

            var person = _repositoryPerson.Persons.FirstOrDefault(p => p.Id == personId);
            if (person != null)
            {
                ViewBag.UserName = person.User.UserName;
                ViewBag.Dpto = GetDeparts(person.Address.Ubigeo.CodDpto);
                ViewBag.Prov = GetProvincesList(person.Address.Ubigeo.CodDpto, person.Address.Ubigeo.CodProv);
                ViewBag.Dist = GetDistrictsList(person.Address.Ubigeo.CodDpto, person.Address.Ubigeo.CodProv, person.Address.Ubigeo.CodDist);
                ViewBag.Role = GetRolesList(person.Role.RoleCode);
                ViewBag.DocumentType = GetDocumentTypeList(person.Document.DocumentType.Id.ToString(CultureInfo.InvariantCulture));
            }
            return View(new PersonView().ToView(person));
        }

        [HttpPost]
        public ActionResult Edit(PersonView model)
        {
            ViewBag.Action = "Edit";
            if (ModelState.IsValid)
            {
                var ubigeo = _repositoryUbigeo.Ubigeos.FirstOrDefault(o => o.CodDpto == model.CodDpto && o.CodProv == model.CodProv && o.CodDist == model.CodDist);
                if (ubigeo != null)
                    model.UbigeoId = ubigeo.Id.ToString(CultureInfo.InvariantCulture);

                var role = _repositorySecurity.Roles.FirstOrDefault(p=>p.RoleCode==model.RoleCode);
                if (role != null)
                    model.RoleId= role.Id.ToString(CultureInfo.InvariantCulture);
               
                _repositoryPerson.SavePerson(model.ToBd(model));
                TempData["message"] = string.Format("{0} ha sido guardado", model.UserName);
                return Json(new { ok = true, newurl = "Index" });
            }
            ViewBag.Dpto = GetDeparts(model.CodDpto);
            ViewBag.Prov = ViewBag.Prov = GetProvincesList(model.CodDpto, model.CodProv);
            ViewBag.Dist = GetDistrictsList(model.CodDpto, model.CodProv, model.CodDist);
            ViewBag.Role = GetRolesList(model.RoleCode);
            ViewBag.DocumentType = GetDocumentTypeList(model.DocumentTypeId);
            return Json(new { ok = false, model });

        }

        public ActionResult Create()
        {
            ViewBag.Action = "Create";
            ViewBag.Dpto = GetDeparts(null);
            ViewBag.Role = GetRolesList(null);
            ViewBag.DocumentType = GetDocumentTypeList(null);
            ViewBag.Prov = new SelectList(new List<SelectListItem>());
            ViewBag.Dist = new SelectList(new List<SelectListItem>());
            return View("Edit", new PersonView());
        }


        public ActionResult Delete(int personId)
        {
            var deletedPerson = _repositoryPerson.DeletePerson(personId);
            if (deletedPerson != null)
            {
                TempData["message"] = string.Format("{0} fue eliminado", deletedPerson.FirstName);
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

        public SelectList GetDocumentTypeList(string selected)
        {
            return _repositoryPerson.GetDocumentTypeList(selected);
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