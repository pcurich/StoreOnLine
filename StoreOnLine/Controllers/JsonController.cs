using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.Controllers
{
    public class JsonController : Controller
    {
        private Person[] personData = {
            new Person {FirstName = "Adam", LastName = "Freeman", BaseCode  = "1111"},
            new Person {FirstName = "Jacqui", LastName = "Griffyth", BaseCode  = "2222"},
            new Person {FirstName = "John", LastName = "Smith", BaseCode  = "3333"},
            new Person {FirstName = "Anne", LastName = "Jones", BaseCode  = "4444"}
        };

        public ActionResult Index()
        {
            return View();
        }

        private IEnumerable<Person> GetData(string selectedRole)
        {
            IEnumerable<Person> data = personData;
            if (selectedRole != "All")
            {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                data = personData.Where(p => p.BaseCode != null && p.BaseCode == selectedRole);
            }
            return data;
        }

        public JsonResult GetPeopleDataJson(string selectedRole = "1111")
        {
            var data = GetData(selectedRole).Select(p => new
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Role = Enum.GetName(typeof(Role), p.Role)
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPeopleData(string selectedRole = "1111")
        {
            return PartialView(GetData(selectedRole));
        }

        public ActionResult GetPeople(string selectedRole = "1111")
        {
            return View((object)selectedRole);
        }

        public ActionResult GetPeopleData2(string selectedRole = "All")
        {
            //X-Requested-With
            IEnumerable<Person> data = personData;
            if (selectedRole != "All")
            {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                data = personData.Where(p => p.Role == selected);
            }
            if (Request.IsAjaxRequest())
            {
                var formattedData = data.Select(p => new
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Role = Enum.GetName(typeof(Role), p.Role)
                });
                return Json(formattedData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(data);
            }
        }


    }
}