using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using StoreOnLine.Areas.Report.Models;
using System.Globalization;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.Companies;

namespace StoreOnLine.Areas.Report.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ISecurityRepository _securityRepository;

        public AdminController(ICompanyRepository companyRepository, IScheduleRepository scheduleRepository,
            IPersonRepository personRepository,ISecurityRepository securityRepository)
        {
            ViewBag.Big = "Reportes";
            ViewBag.Small = "Resumen de actividades mensuales";
            ViewBag.Area = "Report";
            ViewBag.Controller = "Admin";
            ViewBag.Action = "Index";
            _companyRepository = companyRepository;
            _scheduleRepository = scheduleRepository;
            _personRepository = personRepository;
            _securityRepository = securityRepository;
        }

        //
        // GET: /Report/Admin/
        public ActionResult Index()
        {
            ViewBag.Action = "Index";
            ReportView report;
            if (Session["report"] == null)
            {
                report = new ReportView();
            }
            else
            {
                report = (ReportView)Session["report"];
            }
            ViewBag.Month = GetListMonth();
            ViewBag.Internal = GetCompanyInternarl();
            return View(report);
        }

        [HttpPost]
        public ActionResult Index(ReportView model)
        {
            ViewBag.Action = "Index";
            model.DateFrom = new DateTime(model.Year, model.MonthName, 1);
            model.DateTo = (new DateTime(model.Year, model.MonthName + 1, 1)).AddDays(-1);
            model.Headers = GetHeader(model.DateFrom, model.DateTo);
            model.ReportDetails = GetReportDetail(model.Headers, model.Internal, model.MonthName, model.Year);
            ViewBag.Month = GetListMonth();
            ViewBag.Internal = GetCompanyInternarl();
            Session["report"] = model;
            return View(model);
        }

        private List<ReportDetailView> GetReportDetail(ReportDetailView headers, string BaseCode, int month, int year)
        {
            var salida = new List<ReportDetailView>();

            var startTime = new DateTime(year, month, 1);
            var endTime = new DateTime(year, month + 1, 1).AddDays(-1);

            //  PEOPLE ASSIGNED
            var people = _personRepository.Persons.Where(o => o.BaseCode == BaseCode).ToList();

            //INTERNAL
            var schedulesInternal = _scheduleRepository.Schedules.Where(o => o.BaseCode == BaseCode &&
                                                      o.ScheduleFrom.DayOfYear >= startTime.DayOfYear &&
                                                      o.ScheduleTo.DayOfYear <= endTime.DayOfYear).ToList();


            foreach (var schedule in schedulesInternal)
            {
                foreach (var person in people)
                {
                    var repoScheduleDetails = new ReportDetailView();

                    for (var day = 1; day <= headers.Days.Count; day++)
                    {
                        var scheduleDetails = schedule.ScheduleDetails
                            .Take(1)
                            .OrderBy(o => o.ModificationDate)
                            .FirstOrDefault(o => o.PersonId == person.Id && o.BaseCodeFrom == BaseCode)??new ScheduleDetail();

                        repoScheduleDetails.CompanyBaseCode = scheduleDetails.BaseCodeTo;
                        repoScheduleDetails.CompanyName = _companyRepository.Companies.FirstOrDefault(o => o.Id == schedule.CompanyId).CompanyName;
                        repoScheduleDetails.Regimen = schedule.ScheduleDaysWorkPerWeek + "x" + schedule.ScheduleDaysOff +
                                                      "x" + schedule.ScheduleHuors;
                        repoScheduleDetails.Rol = person.Role.RoleName;
                        repoScheduleDetails.UserCode = person.User.UserCode;
                        repoScheduleDetails.UserName = person.User.UserName;
                        repoScheduleDetails.Days[day].Activity = scheduleDetails.TypeOfTask ?? "NOINFO";

                        repoScheduleDetails.Days[day].Number = day;
                        repoScheduleDetails.Days[day].AbbNameDay =
                            new DateTime(startTime.Year, startTime.Month, day).ToString("ddd",
                                CultureInfo.CurrentUICulture)
                                .Replace(".", "");
                    }
                    
                    salida.Add(repoScheduleDetails);
                }
            }

            

            return salida;
        }

        private static ReportDetailView GetHeader(DateTime from, DateTime to)
        {
            var days = new List<Day>();
            for (var i = from.Day; i < to.Day; i++)
            {
                var newDay = new Day
                {
                    Number = i,
                    AbbNameDay = new DateTime(from.Year, from.Month, i).ToString("ddd", CultureInfo.CurrentUICulture).Replace(".", "")
                };
                days.Add(newDay);
            }
            return
                new ReportDetailView
                {
                    CompanyName = "Empresa",
                    CompanyBaseCode = "Codvig",
                    Regimen = "Regimen",
                    UserCode = "NI-COD",
                    UserName = "Apellidos y Nombre",
                    Rol = "Cargo",
                    Days = days
                };
        }

        public static SelectList GetListMonth()
        {
            var salida = new List<SelectListItem>();
            for (var i = 0; i < 12; i++)
            {
                salida.Add(new SelectListItem { Text = CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames[i], Value = i.ToString(CultureInfo.InvariantCulture) });

            }
            return new SelectList(salida, "Value", "Text");
        }

        public SelectList GetCompanyInternarl()
        {
            var repo = _companyRepository.Companies.Where(o => o.CompanyType == CompanyType.Internal.ToString()).ToList();
            return new SelectList(repo.Select(r => new SelectListItem { Text = r.CompanyName, Value = r.CompanyCode }).ToList(), "Value", "Text");

        }

        public ActionResult ExportFile()
        {
            var salida = (ReportView)Session["report"];
            var output = new MemoryStream();
            var w = new StreamWriter(output, Encoding.UTF8);

            if (salida != null)
            {
                var name = "Reporte_" + salida.Headers.CompanyName + "_" +
                           CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames[salida.MonthName] + ".csv";


                w.Write(salida.Headers.CompanyName); w.Write(",");
                w.Write(salida.Headers.CompanyBaseCode); w.Write(",");
                w.Write(salida.Headers.Regimen); w.Write(",");
                w.Write(salida.Headers.UserCode); w.Write(",");
                w.Write(salida.Headers.UserName); w.Write(",");
                w.Write(salida.Headers.Rol); w.Write(",");
                foreach (var day in salida.Headers.Days)
                {
                    w.Write(day.Number + "(" + day.AbbNameDay + ")"); w.Write(",");
                }
                w.WriteLine("");

                foreach (var reportDetailView in salida.ReportDetails)
                {
                    w.Write(reportDetailView.CompanyName); w.Write(",");
                    w.Write(reportDetailView.CompanyBaseCode); w.Write(",");
                    w.Write(reportDetailView.Regimen); w.Write(",");
                    w.Write(reportDetailView.UserCode); w.Write(",");
                    w.Write(reportDetailView.UserName); w.Write(",");
                    w.Write(reportDetailView.Rol); w.Write(",");
                    foreach (var day in reportDetailView.Days)
                    {
                        w.Write(day.Activity); w.Write(",");
                    }
                    w.WriteLine("");
                }
                w.Flush();
                output.Position = 0;
                return File(output, "text/comma-separated-values", name);
            }
            return Redirect("Index");
        }
    }
}
