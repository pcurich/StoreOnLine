using System.Collections.Generic;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.DataBase.Abstract
{
    public interface IPersonRepository
    {
        IEnumerable<Person> Persons { get; }
        int SavePerson(Person person);
        Person DeletePerson(int personId, bool physical = false);
    }
}
