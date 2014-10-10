using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using StoreOnLine.Areas.Merchant.Models;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.Companies;
using StoreOnLine.DataBase.Model.Security;
using StoreOnLine.Domain.HtmlModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace StoreOnLine.Areas.Merchant.Controllers
{
    public class AssignController : Controller
    {
        private readonly ICompanyRepository _repositoryCompany;
        private readonly IPersonRepository _repositoryPerson;
        private readonly IScheduleRepository _repositorySchedule;

        public AssignController(ICompanyRepository repositoryCompany,  
            IPersonRepository repositoryPerson, IScheduleRepository repositorySecurity)
        {
            ViewBag.Big = "Contratos: Contratos con la compañia";
            ViewBag.Small = "Lista de contratos";
            ViewBag.Area = "Merchant";
            ViewBag.Controller = "Assign";
            ViewBag.Action = "Index";
            _repositoryCompany = repositoryCompany;
            _repositoryPerson = repositoryPerson;
            _repositorySchedule = repositorySecurity;
        }

        //
        // GET: /Merchant/Assign/
        public ActionResult Index(int companyId)
        {
            var db = _repositoryCompany.Companies.FirstOrDefault(o => o.Id == companyId);
            ViewBag.CompanyId = companyId;
            if (db != null)
            {
                var schedules = db.Schedules.Select(schedule => new ScheduleView().ToView(schedule));
                return View(schedules);
            }
            return View(new List<ScheduleView>());
        }

        public ActionResult AddPersonal(int companyId, int scheduleId)
        {
            var person = _repositoryPerson.Persons.FirstOrDefault(o => o.User.UserName == User.Identity.Name);
            ViewBag.Person = GetPerson(person.BaseCode);
            
            ViewBag.CompanyId = companyId;
            ViewBag.scheduleId = scheduleId;

            const int baseDays = 6;
            var calendar = new Dictionary<int, List<CalendarView>>();

            var company = _repositoryCompany.Companies.FirstOrDefault(o => o.Id == companyId);
            if (company == null) return View(calendar);

            var schedule = company.Schedules.FirstOrDefault(o => o.Id == scheduleId);
            if (schedule == null) return View(calendar);
 
            var days = (schedule.ScheduleTo - schedule.ScheduleFrom).TotalDays + 1;
            int week = Convert.ToInt16(Math.Ceiling(days / baseDays));

            var scheduleFrom = schedule.ScheduleFrom;
            for (var i = 1; i <= week; i++)
            {
                calendar.Add(i,
                    CalendarView.GetWeek(baseDays,scheduleFrom.Day, scheduleFrom.DayOfWeek.ToString(),
                                        schedule.ScheduleTo.Day, scheduleFrom.Month, scheduleFrom.Year));
                scheduleFrom = scheduleFrom.AddDays(baseDays);
            }

            var accordeonList = GetAccordion(company, schedule);
            ViewBag.Accordion = accordeonList;
            return View(calendar);
        }

        #region Custom

        public SelectList GetPerson(string baseCompany)
        {
            var company = _repositoryCompany.Companies.FirstOrDefault(o => o.CompanyCode == baseCompany);
            var repo = _repositoryPerson.Persons.Where(o => o.BaseCode == company.CompanyCode && o.Role.RoleName==RoleList.Empleado.ToString());
            return new SelectList(
                repo.Select(r =>
                    new SelectListItem { Text = r.User.UserName, Value = r.Id.ToString(CultureInfo.InvariantCulture) }
                    ).ToList(), "Value", "Text");
        }

        #endregion

        public JsonResult UpdatePerson(int companyId, int personId, int scheduleId, string time)
        {
            var date = time.Split('|');
            var timeStart = new DateTime(Convert.ToInt16(date[0]), Convert.ToInt16(date[1]), Convert.ToInt16(date[2]));
            var timeEnd = new DateTime(Convert.ToInt16(date[0]), Convert.ToInt16(date[1]), Convert.ToInt16(date[2]));
            var person = _repositoryPerson.Persons.FirstOrDefault(o => o.Id == personId);
            var company = _repositoryCompany.Companies.FirstOrDefault(o => o.Id == companyId);

            if (person != null && company != null)
            {
                var schedule = company.Schedules.FirstOrDefault(o => o.Id == scheduleId);
                if (schedule != null)
                {

                    var detail = new ScheduleDetail();
                    detail.PersonId = personId;
                    detail.ScheduleId = scheduleId;
                    detail.TypeOfTask = TypeOfTask.Asignacion.ToString();
                    detail.TimeStart = timeStart.AddHours(schedule.ScheduleFrom.Hour);
                    detail.TimeEnd = timeEnd.AddHours(schedule.ScheduleFrom.Hour).AddHours(schedule.ScheduleHuors);
                    detail.TotalTime = schedule.ScheduleHuors;

                    _repositorySchedule.SaveScheduleDetail(detail);

                    var accordeonList = GetAccordion(company, schedule);
                    ViewBag.Accordion = accordeonList;
                    return Json(accordeonList);

                }
            }
            return Json(new { ok = false });
        }

        private List<Accordion> GetAccordion(Company company, Schedule schedule)
        {
            int daysInWeek = 7;
            var accordeonList = new List<Accordion>();
            var peopleInCompany = _repositoryPerson.Persons.Where(o => o.BaseCode == company.CompanyCode && o.Role.RoleName==RoleList.Empleado.ToString());

            foreach (var worker in peopleInCompany)
            {
                var panel = new Accordion();
                panel.AddPanel(worker.Id, worker.User.UserName);
                var companiesWithSchedule = _repositoryCompany.Companies.Where(o => o.HasSchedule);
                var numberOfAssin = 0;
                foreach (var companyWithSchedule in companiesWithSchedule)
                {
                    // var schedulesNoDone =_repositoryCompany.Schedules.Where(o => o.CompanyId == companyWithSchedule.Id && !o.IsDone);
                    var schedulesNoDone = _repositorySchedule.Schedules.Where(o => !o.IsDone).ToArray();
                    //var dateFrom = schedulesNoDone.Min(o => o.ScheduleFrom);
                    //var dateTo = dateFrom.AddDays(daysInWeek);

                    foreach (var scheduleNoDone in schedulesNoDone)
                    {
                        numberOfAssin = scheduleNoDone.ScheduleDetails.Count(o => o.PersonId == worker.Id);
                        if (numberOfAssin==0) break;
                        var division = Convert.ToInt16(numberOfAssin / schedule.ScheduleDaysWorkPerWeek)+1;
                        var residue = Convert.ToInt16(numberOfAssin % schedule.ScheduleDaysWorkPerWeek);
                        string message = "";
                        for (var i = 0; i < division; i++)
                        {
                            message = residue + "x" + (7 - residue) + "x" + schedule.ScheduleHuors;
                            panel.AddSubPanel(worker.Id, scheduleNoDone.Id, companyWithSchedule.CompanyName, message);
                        }
                    }
                }
                if (numberOfAssin > 0)
                {
                    accordeonList.Add(panel);
                }
            }
            return accordeonList;
        }

    }
}
