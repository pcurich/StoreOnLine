
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

        public int CompanyAddressId { get; set; }
        public Address Address { get; set; }

        public int CompanyContactNumberId { get; set; }
        public ContactNumber ContactNumber { get; set; }

        public int CompanyPersonId { get; set; }
        public Person CompanyPerson { get; set; }

        public List<Schedule> Schedules { get; set; }

    }
}
