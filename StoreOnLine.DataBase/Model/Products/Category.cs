using System;
using System.Collections.Generic;
using System.Linq;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Resources;

namespace StoreOnLine.DataBase.Model.Products
{
    public class Category : DataBaseId
    {
        public String CategoryName { get; set; }
        public String CategoryDescription { get; set; }
        public IEnumerable<Imagen> CategoryPhoto { get; set; }
        public Imagen CategoryIcon { get; set; }

        public IEnumerable<Product> Products { get; set; }

 
    }
}
