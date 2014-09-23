using System;
using System.Collections.Generic;
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
            return View(new PersonView().ToView(person));
        }

        [HttpPost]
        public ActionResult Edit(PersonView model)
        {
            if (ModelState.IsValid)
            {
                _repositoryPerson.SavePerson(model.ToBd(model));
                TempData["message"] = string.Format("{0} ha sido guardado", model.User.UserName);
                return RedirectToAction("Index");
            }
            // there is something wrong with the data values
            return View(model);
        }

        public ActionResult Create()
        {
            var x = GetDeparts();
            ViewBag.Dpto = GetDeparts();
            ViewBag.Prov = GetProvinces("");
            ViewBag.Dist = GetDistricts("", "");
            ViewBag.Role = GetRoles();
            return View("Edit", new PersonView());
        }

        private List<SelectListItem> GetRoles()
        {
            var repo =_repositorySecurity.Roles.ToList();
            return repo.Select(r => new SelectListItem { Text = r.RoleName, Value = Convert.ToString(r.Id) }).ToList();
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

        public List<SelectListItem> GetDeparts()
        {
            return _repositoryUbigeo.GetDepart();
        }

        public List<SelectListItem> GetProvinces(string codDpto)
        {
            return _repositoryUbigeo.GetProvince(codDpto);
        }

        public List<SelectListItem> GetDistricts(string codDpto, string codProv)
        {
            return _repositoryUbigeo.GetDistrict(codDpto, codProv);
        }

        #endregion
    }
}