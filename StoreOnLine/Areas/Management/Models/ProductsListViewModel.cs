using System;
using System.Collections.Generic;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.Areas.Management.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<ProductBase> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public String CurrentCategory { get; set; }
    }
}