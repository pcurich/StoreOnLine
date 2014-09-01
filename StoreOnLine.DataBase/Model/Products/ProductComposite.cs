using System.Collections.Generic;

namespace StoreOnLine.DataBase.Model.Products
{
    public class ProductComposite : Product
    {
        public List<ProductBase> Products = new List<ProductBase>();
    }
}
