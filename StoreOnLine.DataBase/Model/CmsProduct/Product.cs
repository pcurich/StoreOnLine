using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using StoreOnLine.DataBase.Model.CmsCategory;
using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Model.Providers;
using StoreOnLine.DataBase.Model.Resources;

namespace StoreOnLine.DataBase.Model.CmsProduct
{
    public class Product : DataBaseId
    {
        public bool OnSale { get; set; }
        public bool OnLineOnly { get; set; }
        public string CodEan13 { get; set; }
        public string CodUpc { get; set; }
        public float Quantity { get; set; }
        public float MinimalQuantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceBase { get; set; }
        public decimal AditionalShippingCost { get; set; }
        public string Reference { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Depth { get; set; }
        public float Weight { get; set; }
        public float OutOfStock { get; set; }
        public float QuantityDiscount { get; set; }
        public bool AvailableForOrder { get; set; }
        public DateTime AvailableDate { get; set; }
        public string Condition { get; set; }//new used refurbished
        public bool ShowPrice { get; set; }
        public bool Indexed { get; set; }
        public string Visibility { get; set; } //both catalog search

        public ProductLang ProductLang { get; set; }
        public int ProductLangId { get; set; }

        public IList<ProductAttachment> ProductAttachments { get; set; }

        //public String ProductName { get; set; }
        //public String ProductDescription { get; set; }
        //public Decimal ProductBasePrice { get; set; }
        //public Decimal ProductSalePrice { get; set; }

        //[XmlIgnore]
        //public Category ProductCategory { get; set; }
        //public int ProductCategoryId { get; set; }

        //[XmlIgnore]
        //public Campaign ProductCampaign { get; set; }
        //public int ProductCampaignId { get; set; }

        //[XmlIgnore]
        //public Unit ProductUnit { get; set; }
        //public int ProductUnitId { get; set; }

        //[XmlIgnore]
        //public List<Imagen> ProductImagens { get; set; }

        //[XmlIgnore]
        //public Supplier ProductSupplier { get; set; }
        //public int ProductSupplierId { get; set; }

        //[XmlIgnore]
        //public List<Feature> Features { get; set; }


    }
}
