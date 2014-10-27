using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Routing;
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
                var internalCompany =
                    _repositoryCompany.Companies.FirstOrDefault(
                        o => o.CompanyType == CompanyType.Internal.ToString() && o.CompanyCode == person.BaseCode);

                lista.Add(new CompanyView().ToView(internalCompany));

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
            if (db != null)
            {
                if (db.CompanyType == CompanyType.External.ToString())
                {
                    return
                        View(db.Schedules.Select(schedule => new ScheduleView().ToView(schedule)));
                }
                else
                {
                    int idSchedule = db.Schedules.Take(1).FirstOrDefault().Id;

                    return RedirectToAction("OnTime", new { companyId = db.Id, scheduleId = idSchedule });
                }

            }
            return View(new ScheduleView());
        }

        public ActionResult OnTime(int companyId, int scheduleId)
        {
            ViewBag.Asistencia = GetTypeOfWork();
            ViewBag.Resumen = GetResumen(scheduleId);
            var company = _repositoryCompany.Companies.FirstOrDefault(o => o.Id == companyId);
            var schedule = _repositorySchedule.Schedules.FirstOrDefault(o => o.Id == scheduleId);

            if (company != null && schedule != null && company.HasSchedule && company.CompanyType == CompanyType.External.ToString())
            {
                if (schedule.ScheduleFrom.DayOfYear < DateTime.Now.DayOfYear &&
                    schedule.ScheduleTo.DayOfYear < DateTime.Now.DayOfYear)
                {
                    schedule.IsDone = true;
                    //schedule.Company = null;
                    //schedule.ScheduleDetails = null;
                    _repositorySchedule.SaveSchedule(schedule);

                    var newCompany = _repositoryCompany.Companies.FirstOrDefault(o => o.Id == companyId);
                    if (newCompany != null)
                    {
                        var restantes = newCompany.Schedules.Count(o => o.IsDone == false);
                        if (restantes == 0)
                        {
                            company.StatusOfSchedule = StatusOfSchedule.SinRequerimientos.ToString();
                            //company.Schedules = null;
                            //company.Address = null;
                            //company.ContactNumber = null;
                            //company.Person = null;
                            _repositoryCompany.SaveCompany(company);
                        }
                    }
                }
            }


            if (schedule != null)
            {
                if (schedule.ScheduleDetails != null)
                {
                    var details =
                        schedule.ScheduleDetails.Where(o => o.TimeStart.DayOfYear == DateTime.Now.DayOfYear)
                            .Select(o => new ScheduleDetailView().ToView(o)).OrderByDescending(o => o.Id).Take(1).FirstOrDefault();

                    ViewBag.Person = GetPersonList(details != null ? details.PersonId.ToString() : null);
                    var salida = new ScheduleDetailView();
                    salida.TimeStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
                    salida.TimeEnd = DateTime.Now;
                    return View(details ?? salida);
                }
            }

            return View(new ScheduleDetailView());
        }

        public SelectList GetTypeOfWork()
        {
            var lista = new List<SelectListItem>
            {
                new SelectListItem {Text = "FERIADO LABORADO DIA", Value = "FL_D"},
                new SelectListItem {Text = "FERIADO LABORADO NOCHE", Value = "FL_N"},
                new SelectListItem {Text = "DESCANSO PROGRAMADO", Value = "DES"},
                new SelectListItem {Text = "DESCANSO TRABAJADO DIA", Value = "DT_D"},
                new SelectListItem {Text = "DESCANSO TRABAJADO NOCHE", Value = "DT_N"},
                new SelectListItem {Text = "ROTACION", Value = "ROT"},
                new SelectListItem {Text = "FALTO", Value = "F"},
                new SelectListItem {Text = "PERMISO", Value = "P"},
                new SelectListItem {Text = "SUSPENCION", Value = "S"},
                new SelectListItem {Text = "DESCANSO MÉDICO", Value = "DM"},
                new SelectListItem {Text = "VACACIONES", Value = "VAC"},
                new SelectListItem {Text = "RENUNCIA", Value = "RN"},
                new SelectListItem {Text = "DIA", Value = "D"},
                new SelectListItem {Text = "NOCHE", Value = "N"},
                new SelectListItem {Text = "ADELANTO DE TURNO", Value = "AT"},
                new SelectListItem {Text = "SERVICIO DE PEGADA DIA", Value = "PEG_D"},
                new SelectListItem {Text = "SERVICIO DE PEGADA NOCHE", Value = "PEG_N"},
                new SelectListItem {Text = "LICENCIA", Value = "L"}
            };

            var salida = new SelectList(lista, "Value", "Text");

            return salida;
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