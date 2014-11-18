using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.CmsCategory;
using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Model.Providers;
using StoreOnLine.DataBase.Model.Resources;

namespace StoreOnLine.DataBase.Model.CmsProduct
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

        [XmlIgnore]
        public Supplier ProductSupplier  { get; set; }
        public int ProductSupplierId { get; set; }

        [XmlIgnore]
        public List<Feature> Features { get; set; }


    }
}
