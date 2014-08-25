using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.HtmlHelpers;

namespace StoreOnLine.Areas.Configuration.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<ProductBase> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public String CurrentCategory { get; set; }
    }
}