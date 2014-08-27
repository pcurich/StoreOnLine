using System.Collections.Generic;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Abstract
{
    public interface IUnitRepository
    {
        IEnumerable<Unit> Units { get; } 
    }
}
