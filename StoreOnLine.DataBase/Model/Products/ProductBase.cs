﻿using System.Xml.Serialization;

namespace StoreOnLine.DataBase.Model.Products
{
    public class ProductBase : Product
    {
        [XmlIgnore]
        public ProductComposite ProductComposite { get; set; }
    }
}