using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;
using System.Xml.Serialization;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Providers;
using StoreOnLine.DataBase.Model.Resources;

namespace StoreOnLine.DataBase.Model.Products
{
    public class Product : DataBaseId
    {
        public String ProductName { get; set; }
        public String ProductDescription { get; set; }
        public Decimal ProductBasePrice { get; set; }
        public Decimal ProductSalePrice { get; set; }

        public String ProductType { get; set; }
        
        [XmlIgnore]
        public Imagen ProductIcon { get; set; }

        //[NotMapped]
        //public XmlDocument ProductDetails { get; set; }

        [XmlIgnore]
        public Category ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }

        [XmlIgnore]
        public Campaign ProductCampaign { get; set; }
        public int ProductCampaignId { get; set; }

        [XmlIgnore]
        public Unit ProductUnit { get; set; }
        public int ProductUnitId { get; set; }

        [XmlIgnore]
        public IEnumerable<Imagen> ProductImagens { get; set; }
        [XmlIgnore]
        public IEnumerable<Supplier> Suppliers { get; set; }


    }
}
