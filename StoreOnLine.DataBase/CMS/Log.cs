﻿using System;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model;

namespace StoreOnLine.DataBase.CMS
{
    public class Log : DataBaseId
    {
        public string LogType { get; set; }
        public string LogDescription { get; set; }
        public string UserName { get; set; }
        public DateTime LoggedDate { get; set; }
    }
}
