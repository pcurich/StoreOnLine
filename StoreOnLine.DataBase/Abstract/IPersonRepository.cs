using System.Collections.Generic;
using System.Web.Mvc;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.DataBase.Abstract
{
    public interface IPersonRepository
    {
        IEnumerable<Person> Persons { get; }
        SelectList GetPersons(string selected, int roleId);
        int SavePerson(Person person);
        int SaveAddress(Address address);
        int SaveContactNumber(ContactNumber contactarNumber);
        int SaveDocument(Document document);
        Person DeletePerson(int personId, bool physical = false);
    }
}
