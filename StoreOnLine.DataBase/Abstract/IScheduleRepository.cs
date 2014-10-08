using System.Collections.Generic;
using StoreOnLine.DataBase.Model.Companies;

namespace StoreOnLine.DataBase.Abstract
{
    public interface IScheduleRepository
    {
        IEnumerable<Schedule> Schedules { get; }
        int SaveSchedule(Schedule schedule);
        int SaveScheduleDetail(ScheduleDetail scheduleDetail);
    }
}
