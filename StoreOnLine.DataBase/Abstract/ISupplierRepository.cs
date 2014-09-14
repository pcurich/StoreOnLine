using System.Collections.Generic;
using StoreOnLine.DataBase.Model.Providers;

namespace StoreOnLine.DataBase.Abstract
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> Suppliers { get; }
        int SaveSupplier(Supplier supplier);
        Supplier DeleteSupplier(int supplierId, bool physical = false);
    }
}
