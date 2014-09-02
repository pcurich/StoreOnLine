using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public List<Imagen> ProductImagens { get; set; }

        //[XmlIgnore]
        //public Supplier Supplier { get; set; }
        //public int SupplierId { get; set; }
        [XmlIgnore]
        public List<Feature> Features { get; set; }


    }
}
