using System;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Companies
{
    public class Schedule : DataBaseId
    {
        public DateTime ScheduleFrom { get; set; }
        public DateTime ScheduleTo { get; set; }

        public int ScheduleDaysWork { get; set; }
        public int ScheduleDaysOff { get; set; }

        public int ScheduleTurnId { get; set; }
        public ScheduleTurn ScheduleTurn { get; set; }

        public int ScheduleHuors { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
