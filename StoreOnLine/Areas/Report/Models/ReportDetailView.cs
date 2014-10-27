using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreOnLine.Areas.Report.Models
{
    public class ReportDetailView
    {
        [Display(Name = "Empresa")]
        public string CompanyName { get; set; }

        [Display(Name = "Codvig")]
        public string CompanyBaseCode { get; set; }

        [Display(Name = "Regimen")]
        public string Regimen { get; set; }

        [Display(Name = "NI-COD")]
        public string UserCode { get; set; }

        [Display(Name = "Apellidos y Nombre")]
        public string UserName { get; set; }

        [Display(Name = "Cargo")]
        public string Rol { get; set; }

        public List<Day> Days { get; set; }

        public ReportDetailView()
        {
            Days=new List<Day>();
        }

    }
}