using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using StoreOnLine.Areas.Merchant.Models;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.Companies;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.Areas.Merchant.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ICompanyRepository _repositoryCompany;
        public ScheduleController(ICompanyRepository repositoryCompany)
        {
            ViewBag.Big = "Requerimientos";
            ViewBag.Small = "Horarios establecidos por la compañia";
            ViewBag.Area = "Merchant";
            ViewBag.Controller = "Schedule";
            ViewBag.Action = "Index";
            _repositoryCompany = repositoryCompany;
        }

        //
        // GET: /Merchant/Schedule/
        public ActionResult Index(int companyId)
        {
            ViewBag.Action = "Index";
            ViewBag.companyId = companyId;
            var db = _repositoryCompany.Companies.FirstOrDefault(o => o.Id == companyId);
            return db != null ? View(db.Schedules.Select(schedule => new ScheduleView().ToView(schedule))) : View(new ScheduleView());
        }

        public ActionResult Create(int companyId)
        {
            ViewBag.Action = "Create";
            return View("Edit", new ScheduleView {CompanyId = companyId});
        }

        public ActionResult Edit(int companyId, int scheduleId)
        {
            ViewBag.Action = "Edit";
            var company = _repositoryCompany.Companies.FirstOrDefault(p => p.Id == companyId);
            Schedule schedule = null;
            if (company != null)
            {
                ViewBag.CompanyName = company.CompanyName;
                schedule=company.Schedules.FirstOrDefault(p => p.Id == scheduleId);
            }
            return View(new ScheduleView().ToView(schedule));
        }

        [HttpPost]
        public ActionResult Edit(CompanyView model)
        {
            return View(new ScheduleView());
        }
    }
}