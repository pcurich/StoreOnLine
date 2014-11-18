using System.Collections.Generic;
using StoreOnLine.DataBase.Model.CmsProduct;
using StoreOnLine.DataBase.Model.Shopping;

namespace StoreOnLine.DataBase.Abstract
{
    public interface ICartRepository
    {
        void AddItem(Product product, int quantity);
        void RemoveLine(Product product);
        decimal ComputeTotalValue();
        void Clear();
        IEnumerable<CartLine> Lines();

    }
}
