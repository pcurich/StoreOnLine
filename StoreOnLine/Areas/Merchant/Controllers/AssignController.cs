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

            var schedule = _repositoryCompany.Schedules.FirstOrDefault(o => o.Id == scheduleId);
            if (schedule == null) return View(calendar);

            var totalDays = (schedule.ScheduleTo - schedule.ScheduleFrom).TotalDays + 1;
            int week = Convert.ToInt16(Math.Ceiling(totalDays / baseDays));

            //var scheduleFrom = schedule.ScheduleFrom;
            //var people = _repositoryPerson.Persons.Where(o => o.BaseCode == company.CompanyCode && o.Role.RoleName == RoleList.Empleado.ToString()).ToList();

            //this is for each week
            for (var i = 1; i <= week; i++)
            {
                var start = schedule.ScheduleFrom.Day;
                var to = schedule.ScheduleTo.Day;
                var days = 0;
                var list = new List<CalendarView>();


                while (schedule.ScheduleFrom.DayOfYear <= schedule.ScheduleTo.DayOfYear && days < baseDays)
                {
                    //var newDate = new DateTime(schedule.ScheduleFrom.Year, schedule.ScheduleFrom.Month, start);
                    //
                    var scheduleDetail = schedule.ScheduleDetails.FirstOrDefault(
                        o => o.TimeStart.Year == schedule.ScheduleFrom.Year &&
                            o.TimeStart.Month == schedule.ScheduleFrom.Month &&
                            o.TimeStart.Day == schedule.ScheduleFrom.Day &&
                             o.TypeOfTask == TypeOfTask.Asignacion.ToString());
                    var result = CalendarView.GetDateName(schedule.ScheduleFrom.DayOfWeek.ToString()).Split('|');
                    var dayName = result[0];
                    //  var dayOfWeek = result[1];

                    if (scheduleDetail == null)
                    {
                        list.Add(new CalendarView
                        {
                            TypeOfTask = "0",
                            PersonId = " - ",
                            WeekName = dayName,
                            DayNumber = (start).ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'),
                            MonthName = CalendarView.GetNameMonth(schedule.ScheduleFrom.Month),
                            MonthNumber = schedule.ScheduleFrom.Month,
                            Year = schedule.ScheduleFrom.Year
                        });
                    }
                    else
                    {
                        list.Add(new CalendarView
                        {
                            TypeOfTask = "0",
                            PersonId = _repositoryPerson.Persons.FirstOrDefault(o => o.Id == scheduleDetail.PersonId).User.UserName,
                            WeekName = dayName,
                            DayNumber = (start).ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'),
                            MonthName = CalendarView.GetNameMonth(schedule.ScheduleFrom.Month),
                            MonthNumber = schedule.ScheduleFrom.Month,
                            Year = schedule.ScheduleFrom.Year
                        });
                    }


                    start++;
                    days++;
                    schedule.ScheduleFrom = schedule.ScheduleFrom.AddDays(1);
                }
                calendar.Add(i, list);
                //schedule.ScheduleFrom = schedule.ScheduleFrom.AddDays(baseDays);
            }

            var accordeonList = GetAccordion(schedule);
            ViewBag.Accordion = accordeonList;
            return View(calendar);
        }

        #region Custom

        public SelectList GetPerson(string baseCompany)
        {
            var company = _repositoryCompany.Companies.FirstOrDefault(o => o.CompanyCode == baseCompany);
            var repo = _repositoryPerson.Persons.Where(o => o.BaseCode == company.CompanyCode && o.Role.RoleName == RoleList.Empleado.ToString());
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
                var schedule = _repositoryCompany.Schedules.FirstOrDefault(o => o.Id == scheduleId);
                if (schedule != null)
                {
                    var scheduleDetail = schedule.ScheduleDetails.FirstOrDefault(
                       o =>o.TypeOfTask == TypeOfTask.Asignacion.ToString() &&
                        o.TimeStart.ToShortDateString() == timeStart.ToShortDateString());



                    if (scheduleDetail != null)
                    {
                        scheduleDetail.TypeOfTask = TypeOfTask.DesAsignado.ToString();
                        scheduleDetail.IsStatus = false;
                        scheduleDetail.Schedule = null;
                        _repositorySchedule.SaveScheduleDetail(scheduleDetail);
                    }

                    var detail = new ScheduleDetail();
                    detail.PersonId = personId;
                    detail.ScheduleId = scheduleId;
                    detail.BaseCodeFrom = person.BaseCode;
                    detail.BaseCodeTo= company.CompanyCode;
                    detail.TypeOfTask = TypeOfTask.Asignacion.ToString();
                    detail.TimeStart = timeStart.AddHours(schedule.ScheduleFrom.Hour);
                    detail.TimeEnd = timeEnd.AddHours(schedule.ScheduleFrom.Hour).AddHours(schedule.ScheduleHuors);
                    detail.TotalTime = schedule.ScheduleHuors;

                    _repositorySchedule.SaveScheduleDetail(detail);

                    var accordeonList = GetAccordion(schedule);
                    ViewBag.Accordion = accordeonList;
                    return Json(accordeonList);

                }
            }
            return Json(new { ok = false });
        }

        private List<Accordion> GetAccordion(Schedule schedule)
        {
            int daysInWeek = 7;
            var accordeonList = new List<Accordion>();
            var supervisor = _repositoryPerson.Persons.FirstOrDefault(o => o.User.UserName == User.Identity.Name);
            //var  companyInternal = supervisor.BaseCode; companyInternal

            var peopleInCompany = _repositoryPerson.Persons.Where(o => o.BaseCode == supervisor.BaseCode && o.Role.RoleName == RoleList.Empleado.ToString());
            var companiesWithSchedule = _repositoryCompany.Companies.Where(o => o.HasSchedule);

            foreach (var worker in peopleInCompany)
            {
                var panel = new Accordion();
                panel.AddPanel(worker.Id, worker.User.UserName);
                var numberOfAssin = 0;

                foreach (var company in companiesWithSchedule)
                {
                    var schedulesNoDone = _repositorySchedule.Schedules.Where(o => !o.IsDone && o.CompanyId == company.Id);
                    foreach (var scheduleNoDone in schedulesNoDone)
                    {
                        numberOfAssin = scheduleNoDone.ScheduleDetails.Count(o => o.PersonId == worker.Id
                            && o.TypeOfTask == TypeOfTask.Asignacion.ToString()
                            && o.ScheduleId == scheduleNoDone.Id);

                        if (numberOfAssin == 0) break;

                        var division = Convert.ToInt16(numberOfAssin / schedule.ScheduleDaysWorkPerWeek) + 1;
                        var residue = Convert.ToInt16(numberOfAssin % schedule.ScheduleDaysWorkPerWeek);
                        string message = "";
                        var messages=new List<string>();
                        for (var i = 0; i < division; i++)
                        {
                            if (i + 1 < division)
                            {
                                 messages.Add(6 + "x" + (1) + "x" + schedule.ScheduleHuors);
                            }
                            else
                            {
                                if (residue > 0)
                                {
                                    messages.Add(residue + "x" + (6 - residue) + "x" + schedule.ScheduleHuors);
                                }
                            }
                        }
                        panel.AddSubPanel(worker.Id, scheduleNoDone.Id, company.CompanyName, messages);
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
