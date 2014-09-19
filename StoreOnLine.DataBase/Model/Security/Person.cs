using StoreOnLine.DataBase.Entities;
using System;

namespace StoreOnLine.DataBase.Model.Security
{
    public class Person : DataBaseId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Address HomeAddress { get; set; }
        public bool IsApproved { get; set; }
        public Role Role { get; set; }
    }

}
