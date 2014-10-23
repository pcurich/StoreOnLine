using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using StoreOnLine.Areas.Merchant.Models;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.Companies;
using StoreOnLine.DataBase.Model.Security;

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
            ViewBag.Resumen = GetResumen(scheduleId);

            var db = _repositorySchedule.Schedules.FirstOrDefault(o => o.Id == scheduleId);
            if (db != null)
            {
                var details =
                    db.ScheduleDetails.Where(o => o.TimeStart.Day == DateTime.Now.Day)
                        .Select(schedule => new ScheduleDetailView().ToView(schedule)).OrderByDescending(o => o.Id).Take(1).FirstOrDefault();

                if (details != null)
                {
                    ViewBag.Person = GetPersonList(details.PersonId.ToString());

                }
                else
                {
                    ViewBag.Person = GetPersonList(null);

                }
                return View(details);
            }
            return View(new ScheduleDetailView());
        }

        [HttpPost]
        public ActionResult OnTime(ScheduleDetailView model)
        {
            ViewBag.Action = "OnTime";

            if (ModelState.IsValid)
            {
                var schedule = _repositorySchedule.Schedules.FirstOrDefault(o => o.Id == model.ScheduleId);
                var timeEnd = DateTime.Now;

                if (schedule != null)
                {
                    var scheduleDetails = schedule.ScheduleDetails.OrderByDescending(o => o.Id).Take(1).FirstOrDefault();

                    if (scheduleDetails != null)
                    {
                        timeEnd = scheduleDetails.TimeEnd;
                        //terminar su tarea y crearle una nueva
                        scheduleDetails.TimeEnd = DateTime.Now;
                        //scheduleDetails.Person = null;
                        //scheduleDetails.Schedule = null;
                        var elapsedTicks = scheduleDetails.TimeEnd.Ticks - scheduleDetails.TimeStart.Ticks;
                        var elapsedSpan = new TimeSpan(elapsedTicks);
                        scheduleDetails.TotalTime = Convert.ToInt16(elapsedSpan.TotalMinutes);
                        _repositorySchedule.SaveScheduleDetail(scheduleDetails);
                    }
                }
                var m = model.ToBd(model);
                m.Person = null;
                m.Schedule = null;
                m.TimeStart = DateTime.Now;
                m.TimeEnd = timeEnd;
                m.Id = 0;
                _repositorySchedule.SaveScheduleDetail(m);

                ViewBag.Resumen = GetResumen(m.ScheduleId);

                TempData["message"] = string.Format("ha sido guardado");
                return Json(new { ok = true, newurl = "OnTime?companyId=" + schedule.CompanyId + "&scheduleId=" + schedule.Id });
            }

            return Json(new { ok = false, model });
        }

        private object GetResumen(int scheduleId)
        {
            var schedule = _repositorySchedule.Schedules.FirstOrDefault(o => o.Id == scheduleId);

            if (schedule != null)
            {
                var listActivities =
                    (from scheduleDetail in schedule.ScheduleDetails.Where(o => o.TimeStart.ToShortDateString() == DateTime.Now.ToShortDateString()).OrderByDescending(o => o.Id)

                     let element = scheduleDetail.TimeStart.ToShortTimeString() + " - " +
                                   scheduleDetail.TimeEnd.ToShortTimeString() + " >> "

                     let person = (_repositoryPerson.Persons.FirstOrDefault(o => o.Id == scheduleDetail.PersonId)
                                   ?? new Person { User = new User { UserName = " " } })
                     select element + person.User.UserName + " ( " + scheduleDetail.TypeOfTask + " )"

                    ).ToList();

                return listActivities;
            }
            return new List<ScheduleDetailView>();
        }

        public SelectList GetPersonList(string selected)
        {
            return _repositoryPerson.GetPersons(selected, 3);
        }


    }
}