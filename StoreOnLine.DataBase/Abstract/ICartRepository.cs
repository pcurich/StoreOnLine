using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Model.Products;
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
