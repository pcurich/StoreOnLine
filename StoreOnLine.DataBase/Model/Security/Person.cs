﻿using System.Collections.Generic;
using System.Windows.Forms;
using StoreOnLine.DataBase.Entities;
using System;
using StoreOnLine.DataBase.Model.Providers;

namespace StoreOnLine.DataBase.Model.Security
{
    public class Person : DataBaseId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public string Email { get; set; }

        public Document Documents { get; set; }
        public ContactNumber ContactNumbers { get; set; }
        public Address HomeAddress { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }

}