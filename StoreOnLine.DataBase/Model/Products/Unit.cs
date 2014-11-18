using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using StoreOnLine.DataBase.Model.CmsProduct;
using StoreOnLine.DataBase.Model.Resources;

namespace StoreOnLine.DataBase.Model.Products
{
    public class Unit : DataBaseId
    {
        public String UnitName { get; set; }
        public String UnitDescription { get; set; }
        public String UnitCode { get; set; }

        [XmlIgnore]
        public Imagen UnitPhoto { get; set; }

        [XmlIgnore]
        public List<Product> Products { get; set; }


    }
}
