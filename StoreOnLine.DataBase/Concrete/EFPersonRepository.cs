using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
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
                    .Include(o => o.Address)
                    .Include(o => o.ContactNumber)
                    .Include(o => o.Document)
                    .Include(o=>o.User)
                    .Include(o => o.Role)
                    .Include(o => o.Address.Ubigeo)
                    .Where(o => !o.IsDeleted);
            }
        }

        public SelectList GetPersons(string selected, int roleId)
        {
            var repo = _context.Persons.Where(o => !o.IsDeleted && o.RoleId==roleId);
            return new SelectList(repo.Select(r => new SelectListItem { Text = r.FirstName + " " + r.LastName , Value = r.Id.ToString(), Selected = (r.Id.ToString() == selected) }).ToList(), "Value", "Text");
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

        public int SaveAddress(Address address)
        {
            if (address.Id == 0)
            {
                _context.Addresses.Add(address);
            }
            else
            {
                var dbEntry = _context.Addresses.Find((address.Id));
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(address))
                {
                    if (!property.Name.Equals("Id") && property.GetValue(address) != null)
                    {
                        var value = property.GetValue(address);
                        if (value != null) property.SetValue(dbEntry, value);
                    }
                }

            }
            _context.SaveChanges();
            return address.Id;
        }

        public int SaveContactNumber(ContactNumber contactarNumber)
        {
            if (contactarNumber.Id == 0)
            {
                _context.ContactNumbers.Add(contactarNumber);
            }
            else
            {
                var dbEntry = _context.ContactNumbers.Find((contactarNumber.Id));
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(contactarNumber))
                {
                    if (!property.Name.Equals("Id") && property.GetValue(contactarNumber) != null)
                    {
                        var value = property.GetValue(contactarNumber);
                        if (value != null) property.SetValue(dbEntry, value);
                    }
                }

            }
            _context.SaveChanges();
            return contactarNumber.Id;
        }

        public int SaveDocument(Document document)
        {
            if (document.Id == 0)
            {
                _context.Documents.Add(document);
            }
            else
            {
                var dbEntry = _context.Addresses.Find((document.Id));
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(document))
                {
                    if (!property.Name.Equals("Id") && property.GetValue(document) != null)
                    {
                        var value = property.GetValue(document);
                        if (value != null) property.SetValue(dbEntry, value);
                    }
                }

            }
            _context.SaveChanges();
            return document.Id;
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
