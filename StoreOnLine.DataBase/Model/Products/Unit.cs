using System.Xml.Serialization;
using StoreOnLine.DataBase.Entities;
using System;
using System.Collections.Generic;

namespace StoreOnLine.DataBase.Model.Products
{
    public class Unit : DataBaseId
    {
        public String UnitName { get; set; }
        public String UnitDescription { get; set; }
        public String UniCode { get; set; }

        [XmlIgnore]
        public List<Product> Products { get; set; }


    }
}
