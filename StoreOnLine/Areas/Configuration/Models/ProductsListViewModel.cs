using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.Areas.Configuration.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int CurrentCategoryId { get; set; }
    }
}