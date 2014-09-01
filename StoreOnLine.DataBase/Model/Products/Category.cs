using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Serialization;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Resources;

namespace StoreOnLine.DataBase.Model.Products
{
    public class Category : DataBaseId
    {
        public String CategoryName { get; set; }
        public String CategoryDescription { get; set; }

        [XmlIgnore]
        public Imagen CategoryPhoto { get; set; }

        [XmlIgnore]
        public List<Product> Products { get; set; }


    }
}
