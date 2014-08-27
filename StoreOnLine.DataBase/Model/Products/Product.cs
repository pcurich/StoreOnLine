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
        [Required(ErrorMessage = "Please enter a product name")]
        public String ProductName { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [DataType(DataType.MultilineText)]
        public String ProductDescription { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [DataType(DataType.Currency)]
        public Decimal ProductBasePrice { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [DataType(DataType.Currency)]
        public Decimal ProductSalePrice { get; set; }

        public String ProductType { get; set; }
        
        [XmlIgnore]
        public Imagen ProductIcon { get; set; }

        //[NotMapped]
        //public XmlDocument ProductDetails { get; set; }


        [XmlIgnore]
        public Category ProductCategory { get; set; }
        [Required(ErrorMessage = "Please specify a category")]
        public int ProductCategoryId { get; set; }

        [XmlIgnore]
        public Campaign ProductCampaign { get; set; }
        [Required(ErrorMessage = "Please specify a campaign")]
        public int ProductCampaignId { get; set; }

        [XmlIgnore]
        public Unit ProductUnit { get; set; }
        [Required(ErrorMessage = "Please specify a unit")]
        public int ProductUnitId { get; set; }

        [XmlIgnore]
        public IEnumerable<Imagen> ProductImagens { get; set; }
        [XmlIgnore]
        public IEnumerable<Supplier> Suppliers { get; set; }


    }
}
