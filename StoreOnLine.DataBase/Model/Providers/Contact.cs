using System;
using System.Xml;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Providers
{
    public class Contact : DataBaseId
    {
        public String ContactName { get; set; }
        public String ContactLastName { get; set; }
        public String ContactLastSecond { get; set; }
        public Boolean IsPrincipal { get; set; }

        //public XmlDocument ContactPhone { get; set; }
    }
}
