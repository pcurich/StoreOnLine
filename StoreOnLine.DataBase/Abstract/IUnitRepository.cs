using System.Collections.Generic;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Abstract
{
    public interface IUnitRepository
    {
        IEnumerable<Unit> Units { get; }
        int SaveUnit(Unit unit);
        Unit DeleteUnit(int unitId, bool physical = false);
    }
}
