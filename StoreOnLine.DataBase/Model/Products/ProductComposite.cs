using System.Collections.Generic;

namespace StoreOnLine.DataBase.Model.Products
{
    public class ProductComposite : Product
    {
        public IEnumerable<ProductBase> Products = new List<ProductBase>();
    }
}
