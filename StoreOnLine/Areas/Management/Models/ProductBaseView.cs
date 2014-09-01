using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreOnLine.Areas.Management.Models
{
    public class ProductBaseView
    {
        //[Required(ErrorMessage = "Please enter a product name")]
        public String ProductName { get; set; }

        //[Required(ErrorMessage = "Please enter a description")]
        [DataType(DataType.MultilineText)]
        public String ProductDescription { get; set; }

        //[Required]
        //[Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [DataType(DataType.Currency)]
        public Decimal ProductBasePrice { get; set; }

        //[Required]
        // [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [DataType(DataType.Currency)]
        public Decimal ProductSalePrice { get; set; }

        //[NotMapped]
        //public XmlDocument ProductDetails { get; set; }


        [XmlIgnore]
        public Category ProductCategory { get; set; }
        // [Required(ErrorMessage = "Please specify a category")]
        public int ProductCategoryId { get; set; }

        [XmlIgnore]
        public Campaign ProductCampaign { get; set; }
        //[Required(ErrorMessage = "Please specify a campaign")]
        public int ProductCampaignId { get; set; }

        [XmlIgnore]
        public Unit ProductUnit { get; set; }
        //[Required(ErrorMessage = "Please specify a unit")]
        public int ProductUnitId { get; set; }

        [XmlIgnore]
        public List<Imagen> ProductImagens { get; set; }

        [XmlIgnore]
        public Supplier Supplier { get; set; }
        public int SupplierId { get; set; }

        public List<Feature> Details { get; set; }
    }
}