using System.Collections.Generic;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Abstract
{
    public interface IProductsRepository
    {
        IEnumerable<ProductBase> ProductBases { get; }
        IEnumerable<ProductComposite> ProductComposites { get; }

        int SaveProductBase(ProductBase product);
        int SaveProductComposite(ProductComposite product);

        Product DeleteProductBase(int productId);
        Product DeleteProductComposite(int productId);
    }
}
