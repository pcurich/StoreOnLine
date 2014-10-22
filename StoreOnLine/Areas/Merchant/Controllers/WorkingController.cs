using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using StoreOnLine.Areas.Merchant.Models;
using StoreOnLine.DataBase.Abstract;

namespace StoreOnLine.Areas.Merchant.Controllers
{
    public class WorkingController : Controller
    {
        private readonly ICompanyRepository _repositoryCompany;
        private readonly IPersonRepository _repositoryPerson;
        private readonly IScheduleRepository _repositorySchedule;

        public WorkingController(ICompanyRepository repositoryCompany,
            IPersonRepository repositoryPerson, IScheduleRepository repositorySecurity)
        {
            ViewBag.Big = "Asistencia: Toma de asistencia al trabajador";
            ViewBag.Small = "Lista empresas y trabajadores asignados";
            ViewBag.Area = "Merchant";
            ViewBag.Controller = "Working";
            ViewBag.Action = "Index";
            _repositoryCompany = repositoryCompany;
            _repositoryPerson = repositoryPerson;
            _repositorySchedule = repositorySecurity;
        }
        //
        // GET: /Merchant/Working/
        public ActionResult Index()
        {
            ViewBag.Action = "Index";
            var lista = new List<CompanyView>();
            var person = _repositoryPerson.Persons.FirstOrDefault(o => o.User.UserName == User.Identity.Name);
            if (person != null)
            {
                var db = _repositoryCompany.Companies.Where(p => p.CompanyType == CompanyType.External.ToString() && p.HasSchedule).ToList();
                lista.AddRange(from element
                                   in db
                               from source
                                   in element.Schedules.Where(o => o.BaseCode == person.BaseCode)
                               select new CompanyView().ToView(element));
            }

            return View(lista);

        }

        public ActionResult Schedule(int companyId)
        {
            ViewBag.companyId = companyId;
            var db = _repositoryCompany.Companies.FirstOrDefault(o => o.Id == companyId);
            return db != null ? View(db.Schedules.Select(schedule => new ScheduleView().ToView(schedule))) : View(new ScheduleView());
        }

        public ActionResult OnTime(int companyId, int scheduleId)
        {
            

            var db = _repositorySchedule.Schedules.FirstOrDefault(o => o.Id == scheduleId);
            if (db != null)
            {
                var details =
                    db.ScheduleDetails.Where(o => o.TimeStart.Day == DateTime.Now.Day)
                        .Select(schedule => new ScheduleDetailView().ToView(schedule)).FirstOrDefault();
                return View(details);
            }
            return View(new ScheduleDetailView());
        }

        
    }
}