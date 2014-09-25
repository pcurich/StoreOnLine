﻿using StoreOnLine.DataBase.Entities;
using System;

namespace StoreOnLine.DataBase.Model.Security
{
    public class Person : DataBaseId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public int DocumentsId { get; set; }
        public Document Document { get; set; }

        public int ContactNumberId { get; set; }
        public ContactNumber ContactNumber { get; set; }

        public int HomeAddresId { get; set; }
        public Address HomeAddress { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }

}
