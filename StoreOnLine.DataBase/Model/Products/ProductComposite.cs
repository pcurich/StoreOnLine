using System.Collections.Generic;
using StoreOnLine.DataBase.Model.CmsProduct;

namespace StoreOnLine.DataBase.Model.Products
{
    public class ProductComposite : Product
    {
        public List<ProductBase> Products = new List<ProductBase>();
    }
}
