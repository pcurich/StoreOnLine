using System;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.CmsProduct;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Model.Shopping
{
    public class CartLine:DataBaseId
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public Decimal SubTotal
        {
            get
            {
                if (Product != null) return Product.ProductSalePrice*Quantity;
                return 0;
            }
        }
    }
}
