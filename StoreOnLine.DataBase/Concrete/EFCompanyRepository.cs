using System.Linq;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Companies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;

namespace StoreOnLine.DataBase.Concrete
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly StoreOnLineContext _context = new StoreOnLineContext();

        public IEnumerable<Company> Companies
        {
            get
            {
                return _context.Companies
                    .Include(o => o.Address)
                    .Include(o => o.Address.Ubigeo)
                    .Include(o => o.ContactNumber)
                    .Include(o => o.Person)
                    .Include(o=>o.Schedules)
                    .Where(o => !o.IsDeleted);
            }
        }

        public int SaveCompany(Company company)
        {
            if (company.Id == 0)
            {
                _context.Companies.Add(company);
            }
            else
            {
                var dbEntry = _context.Companies.Find((company.Id));
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(company))
                {
                    if (!property.Name.Equals("Id") && property.GetValue(company) != null)
                    {
                        var value = property.GetValue(company);
                        if (value != null) property.SetValue(dbEntry, value);
                    }
                }

            }
            _context.SaveChanges();
            return company.Id;
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

        public Company DeleteCompany(int company, bool physical = false)
        {
            var dbEntry = _context.Companies.Find(company);
            if (dbEntry != null)
            {
                if (physical)
                {
                    _context.Companies.Remove(dbEntry);
                }
                else
                {
                    dbEntry.IsDeleted = true;
                    dbEntry.IsStatus = false;
                    _context.Entry(dbEntry).State = EntityState.Modified;
                }

                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
