using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreOnLine.Areas.Report.Models
{
    public class ReportView
    {

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        [Display(Name = "Mes")]
        public int MonthName { get; set; }

        [Display(Name = "Base")]
        public string Internal { get; set; }

        [Display(Name = "Año")]
        public int Year { get; set; }

        public ReportDetailView Headers { get; set; }
        public List<ReportDetailView> ReportDetails { get; set; }

        public ReportView()
        {
            Year = 2014;
            MonthName = -1;
            ReportDetails = new List<ReportDetailView>();
        }



    }


}