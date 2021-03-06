﻿using System;
using StoreOnLine.DataBase.Model.CmsProduct;

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
                if (Product != null) return Product.Price*Quantity;
                return 0;
            }
        }
    }
}
