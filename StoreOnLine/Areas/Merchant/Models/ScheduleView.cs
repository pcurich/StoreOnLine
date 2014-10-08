using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using StoreOnLine.DataBase.Model.Companies;
using WebGrease;

namespace StoreOnLine.Areas.Merchant.Models
{
    public class ScheduleView
    {
        public ScheduleView()
        {
            IsStatus = true;
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Activo")]
        public bool IsStatus { get; set; }

        [Display(Name = "Fecha Inicio")]
        [DataType(DataType.Text)]
        public string ScheduleFrom { get; set; }

        [Display(Name = "Fecha fin")]
        [DataType(DataType.Text)]
        public string ScheduleTo { get; set; }

        [Range(1, 7, ErrorMessage = "El valor debe estar entre 1 y 7")]
        [Display(Name = "Dias laborales (Semana)")]
        [DataType(DataType.Text)]
        public string ScheduleDaysWorkPerWeek { get; set; }

        public string ScheduleDaysOff { get; set; }

        [Display(Name = "Turno de Trabajo")]
        [DataType(DataType.Text)]
        public string ScheduleTurn { get; set; }

        [Range(1, 12, ErrorMessage = "Un turno puede durar entre 1 a 12 horas")]
        [Display(Name = "Jornada de Trabajo")]
        [DataType(DataType.Text)]
        public string ScheduleHuors { get; set; }

        [Display(Name = "Hora de inicio")]
        [DataType(DataType.Text)]
        public string ScheduleTimeStart { get; set; }

        [Display(Name = "Empresa")]
        [DataType(DataType.Text)]
        public int CompanyId { get; set; }


        public Schedule ToBd(ScheduleView view)
        {
            var dateStr = view.ScheduleFrom.Split('/');
            var timeStr = view.ScheduleTimeStart.Split(':');
            
            var dateFrom = new DateTime(
                Convert.ToInt16(dateStr[2]), 
                Convert.ToInt16(dateStr[1]), 
                Convert.ToInt16(dateStr[0]), 
                Convert.ToInt16(timeStr[0]), 
                Convert.ToInt16(timeStr[1].Substring(0,2)),0);

            dateStr = view.ScheduleTo.Split('/');
            var dateTo = new DateTime(
                Convert.ToInt16(dateStr[2]), 
                Convert.ToInt16(dateStr[1]), 
                Convert.ToInt16(dateStr[0]),
                Convert.ToInt16(timeStr[0]),
                Convert.ToInt16(timeStr[1].Substring(0, 2)), 0);
            return new Schedule
            {
                Id = view.Id,
                IsStatus = view.IsStatus,
                ScheduleFrom = dateFrom,
                ScheduleTo = dateTo,
                ScheduleDaysWorkPerWeek = Convert.ToInt16(view.ScheduleDaysWorkPerWeek),
                ScheduleDaysOff = 7 - Convert.ToInt16(view.ScheduleDaysWorkPerWeek),
                ScheduleTurn = view.ScheduleTurn,
                ScheduleHuors = Convert.ToInt16(view.ScheduleHuors),
                CompanyId = view.CompanyId
            };
        }

        public ScheduleView ToView(Schedule db)
        {
            return new ScheduleView
            {
                Id = db.Id,
                IsStatus = db.IsStatus,
                ScheduleFrom = db.ScheduleFrom.ToShortDateString(),
                ScheduleTo = db.ScheduleTo.ToShortDateString(),
                ScheduleDaysWorkPerWeek = Convert.ToString(db.ScheduleDaysWorkPerWeek),
                ScheduleDaysOff = Convert.ToString(db.ScheduleDaysOff),
                ScheduleTurn = db.ScheduleTurn,
                ScheduleTimeStart=db.ScheduleFrom.ToShortTimeString(),
                ScheduleHuors = Convert.ToString(db.ScheduleHuors),
                CompanyId = db.CompanyId
            };
        }
    }
}
public enum DayPerWeek
{
    Uno = 1,
    Dos = 2,
    Tres = 3,
    Cuatro = 4,
    Cinco = 5,
    Seis = 6,
    Siete = 7
}