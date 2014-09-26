using System.Collections.Generic;
using System.Web.Mvc;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.DataBase.Abstract
{
    public interface IPersonRepository
    {
        IEnumerable<Person> Persons { get; }
        SelectList GetDocumentTypeList(string selected);
        int SavePerson(Person person);
        Person DeletePerson(int personId, bool physical = false);
    }
}
