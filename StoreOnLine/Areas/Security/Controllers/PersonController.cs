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
        private readonly IPerson _repository;

        public PersonController(IPerson repo)
        {
            ViewBag.Big = "Administrar: Usuarios";
            ViewBag.Small = "Lista de Usuarios";
            ViewBag.Area = "Security";
            ViewBag.Controller = "Person";
            ViewBag.Action = "Index";
            _repository = repo;
        }

        //
        // GET: /Security/User/
        public ActionResult Index()
        {
            var db = _repository.Persons;
            var view = db.Select(person => new PersonView().ToView(person)).ToList();
            return View(view);
        }

        public ViewResult Edit(int personId)
        {
            ViewBag.Action = "Edit";
            var person = _repository.Persons.FirstOrDefault(p => p.Id == personId);
            if (person != null) ViewBag.UserName = person.User.UserName;

            return View(new PersonView().ToView(person));
        }

        [HttpPost]
        public ActionResult Edit(PersonView model)
        {
            if (ModelState.IsValid)
            {
                _repository.SavePerson(model.ToBd(model));
                TempData["message"] = string.Format("{0} ha sido guardado", model.User.UserName);
                return RedirectToAction("Index");
            }
            // there is something wrong with the data values
            return View(model);
        }

        public ActionResult Create()
        {
            return View("Edit", new PersonView());
        }

        public ActionResult Delete(int personId)
        {
            var deletedPerson = _repository.DeletePerson(personId);
            if (deletedPerson != null)
            {
                TempData["message"] = string.Format("{0} fue eliminado", deletedPerson.User.UserName);
            }
            return RedirectToAction("Index");
        }
    }
}