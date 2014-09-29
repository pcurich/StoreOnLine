
using System.Collections.Generic;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.DataBase.Model.Companies
{
    public class Company : DataBaseId
    {
        public string CompanyName { get; set; }
        public string CompanyActivity { get; set; }
        public string CompanyCif{ get; set; }
        public string CompanySecurityNumber { get; set; }

        public string CompanyDocumentRuc { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public int ContactNumberId { get; set; }
        public ContactNumber ContactNumber { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public List<Schedule> Schedules { get; set; }

    }
}
