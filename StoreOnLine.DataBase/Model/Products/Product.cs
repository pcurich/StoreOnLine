using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;
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
        public Imagen ProductIcon { get; set; }

        //[NotMapped]
        //public XmlDocument ProductDetails { get; set; }

        public Category ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }

        public Campaign ProductCampaign { get; set; }
        public int ProductCampaignId { get; set; }

        public Unit ProductUnit { get; set; }
        public int ProductUnitId { get; set; }

        public IEnumerable<Imagen> ProductImagens { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }


    }
}
