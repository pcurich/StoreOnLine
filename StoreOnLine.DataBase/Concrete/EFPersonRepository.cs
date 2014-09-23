using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.DataBase.Concrete
{
    public class PersonRepository : IPersonRepository
    {
        private readonly StoreOnLineContext _context = new StoreOnLineContext();

        public IEnumerable<Person> Persons
        {
            get
            {
                return _context.Persons
                    .Include(o => o.HomeAddress)
                    .Include(o=>o.User)
                    .Include(o => o.Role)
                    .Where(o => !o.IsDeleted);
            }
        }
        public int SavePerson(Person person)
        {
            if (person.Id == 0)
            {
                _context.Persons.Add(person);
            }
            else
            {
                var dbEntry = _context.Persons.Find((person.Id));
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(person))
                {
                    if (!property.Name.Equals("Id") && property.GetValue(person) != null)
                    {
                        var value = property.GetValue(person);
                        if (value != null) property.SetValue(dbEntry, value);
                    }
                }

            }
            _context.SaveChanges();
            return person.Id;
        }

        public Person DeletePerson(int personId, bool physical = false)
        {
            var dbEntry = _context.Persons.Find(personId);
            if (dbEntry == null) return null;
            if (physical)
            {
                _context.Persons.Remove(dbEntry);
            }
            else
            {
                dbEntry.IsDeleted = true;
                dbEntry.IsStatus = false;
                _context.Entry(dbEntry).State = EntityState.Modified;
            }

            _context.SaveChanges();
            return dbEntry;
        }
    }
}
