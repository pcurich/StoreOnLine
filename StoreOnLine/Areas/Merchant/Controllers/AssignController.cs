using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreOnLine.Areas.Merchant.Models;
using StoreOnLine.DataBase.Abstract;

namespace StoreOnLine.Areas.Merchant.Controllers
{
    public class AssignController : Controller
    {
        private readonly ICompanyRepository _repositoryCompany;
        private readonly IUbigeoRepository _repositoryUbigeo;
        private readonly IPersonRepository _repositoryPerson;
        private readonly ISecurityRepository _repositorySecurity;

        public AssignController(ICompanyRepository repositoryCompany, IUbigeoRepository repositoryUbigeo, IPersonRepository repositoryPerson, ISecurityRepository repositorySecurity)
        {
            ViewBag.Big = "Contratos: Contratos con la compañia";
            ViewBag.Small = "Lista de contratos";
            ViewBag.Area = "Merchant";
            ViewBag.Controller = "Assign";
            ViewBag.Action = "Index";
            _repositoryCompany = repositoryCompany;
            _repositoryUbigeo = repositoryUbigeo;
            _repositoryPerson = repositoryPerson;
            _repositorySecurity = repositorySecurity;
        }
        //
        // GET: /Merchant/Assign/
        public ActionResult Index(int companyId)
        {
            var db = _repositoryCompany.Companies.FirstOrDefault(o => o.Id == companyId);
            if (db != null)
            {
                var schedules = db.Schedules.Select(schedule => new ScheduleView().ToView(schedule));
                return View(schedules);
            }
            return View(new List<ScheduleView>());
        }

        public ActionResult AddPersonal(int scheduleId)
        {
            return View();
        }

    }
}
