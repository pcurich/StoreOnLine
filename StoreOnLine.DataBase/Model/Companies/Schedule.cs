using System;
using System.Collections.Generic;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Companies
{
    public class Schedule : DataBaseId
    {
        public DateTime ScheduleFrom { get; set; }
        public DateTime ScheduleTo { get; set; }

        public bool IsDone { get; set; }

        public int ScheduleDaysWorkPerWeek { get; set; } //Dias que se trabajan durante la semana 
        public int ScheduleDaysOff { get; set; } // complemento con 7

        public string ScheduleTurn { get; set; } //Mañana o Noche

        public int ScheduleHuors { get; set; } //Jornada de cuantas horas?

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public string BaseCode { get; set; }

        public List<ScheduleDetail> ScheduleDetails { get; set; }
    }
}

public enum ScheduleTurn
{
    Mañana = 1,
    Noche = 2
}
 