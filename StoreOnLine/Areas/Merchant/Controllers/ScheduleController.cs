using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using StoreOnLine.Areas.Merchant.Models;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.Companies;

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
            ViewBag.Controller = "ScheduleC";
            ViewBag.Action = "Index";
            _repositoryCompany = repositoryCompany;
        }

        //
        // GET: /Merchant/Schedule/
        public ActionResult Index(int companyId)
        {
            ViewBag.Action = "Index";
            var db = _repositoryCompany.Companies.FirstOrDefault(o => o.Id == companyId);
            return db != null ? View(db.Schedules.Select(schedule=>new ScheduleView().ToView( schedule))) : View(new ScheduleView());
        }
    }
}