using StoreOnLine.DataBase.Model.Companies;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StoreOnLine.Areas.Merchant.Models
{
    public class ScheduleDetailView
    {
        public ScheduleDetailView()
        {
            IsStatus = true;
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Activo")]
        public bool IsStatus { get; set; }

        public int ScheduleId { get; set; }

        [Display(Name = "Personal")]
        [DataType(DataType.Text)]
        public int PersonId { get; set; }

        [Display(Name = "Inicio")]
        [DataType(DataType.Text)]
        public DateTime TimeStart { get; set; }

        [Display(Name = "Fin")]
        [DataType(DataType.Text)]
        public DateTime TimeEnd { get; set; }

        [Display(Name = "Tiempo Total")]
        [DataType(DataType.Text)]
        public int TotalTime { get; set; }

        [Display(Name = "Estado")]
        [DataType(DataType.Text)]
        public string TypeOfTask { get; set; }

        public ScheduleDetail ToBd(ScheduleDetailView view)
        {
            return new ScheduleDetail
            {
                Id = view.Id,
                IsStatus = view.IsStatus,
                ScheduleId = view.ScheduleId,
                PersonId=view.PersonId,
                TimeStart = view.TimeStart,
                TimeEnd = view.TimeEnd,
                TotalTime = view.TotalTime,
                TypeOfTask = view.TypeOfTask
            };
        }

        public ScheduleDetailView ToView(ScheduleDetail db)
        {
            return new ScheduleDetailView
            {
                Id = db.Id,
                IsStatus = db.IsStatus,
                ScheduleId = db.ScheduleId,
                PersonId = db.PersonId,
                TimeStart = db.TimeStart,
                TimeEnd = db.TimeEnd,
                TotalTime = db.TotalTime,
                TypeOfTask = db.TypeOfTask
            };
        }
    }
}