using System;
using System.Collections.Generic;
using System.Globalization;
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

        public ActionResult AddPersonal(int companyId, int scheduleId)
        {
            ViewBag.Person = GetPerson(companyId);

            var calendar = new Dictionary<int, List<CalendarView>>();

            var company = _repositoryCompany.Companies.FirstOrDefault(o => o.Id == companyId);
            if (company == null) return View(calendar);

            var schedule = company.Schedules.FirstOrDefault(o => o.Id == scheduleId);
            if (schedule == null) return View(calendar);

            var days = (schedule.ScheduleTo - schedule.ScheduleFrom).TotalDays + 1;
            int week = Convert.ToInt16(Math.Ceiling(days / 6));

            var scheduleFrom = schedule.ScheduleFrom;
            for (var i = 1; i <= week; i++)
            {
                calendar.Add(i, CalendarView.GetWeek(scheduleFrom.Day,scheduleFrom.DayOfWeek.ToString(), schedule.ScheduleTo.Day, scheduleFrom.Month));
                scheduleFrom = scheduleFrom.AddDays(6);
            }
            return View(calendar);
        }

        #region Custom

        public SelectList GetPerson(int companyId)
        {
            var company =_repositoryCompany.Companies.FirstOrDefault(o => o.Id == companyId);
            var repo = _repositoryPerson.Persons.Where(o => company != null && o.BaseCode == company.CompanyCode);
            return new SelectList(
                repo.Select(r => 
                    new SelectListItem { Text = r.User.UserName, Value = r.Id.ToString(CultureInfo.InvariantCulture)}
                    ).ToList(), "Value", "Text");
        }
        #endregion

    }
}
