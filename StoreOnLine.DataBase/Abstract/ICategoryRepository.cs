using System.Collections.Generic;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Abstract
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
