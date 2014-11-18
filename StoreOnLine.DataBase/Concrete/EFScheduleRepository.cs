using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Companies;

namespace StoreOnLine.DataBase.Concrete
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly StoreOnLineContext _context = new StoreOnLineContext();

        public IEnumerable<Schedule> Schedules
        {
            get
            {
                return _context.Schedules
                    .Include(o => o.Company)
                    .Include(o => o.ScheduleDetails)
                    .Where(o => !o.IsDeleted);
            }
        }

        public int SaveSchedule(Schedule schedule)
        {
            if (schedule.Id == 0)
            {
                _context.Schedules.Add(schedule);
            }
            else
            {
                var dbEntry = _context.Schedules.Find((schedule.Id));
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(schedule))
                {
                    if (!property.Name.Equals("Id") && property.GetValue(schedule) != null)
                    {
                        var value = property.GetValue(schedule);
                        if (value != null) property.SetValue(dbEntry, value);
                    }
                }

            }
            _context.SaveChanges();
            return schedule.Id;
        }

        public int SaveScheduleDetail(ScheduleDetail scheduleDetail)
        {
            if (scheduleDetail.Id == 0)
            {
                _context.ScheduleDetails.Add(scheduleDetail);
            }
            else
            {
                var dbEntry = _context.ScheduleDetails.Find((scheduleDetail.Id));
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(scheduleDetail))
                {
                    if (!property.Name.Equals("Id") && property.GetValue(scheduleDetail) != null)
                    {
                        var value = property.GetValue(scheduleDetail);
                        if (value != null) property.SetValue(dbEntry, value);
                    }
                }

            }
            _context.SaveChanges();
            return scheduleDetail.Id;
        }
    }
}
