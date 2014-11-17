using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Providers;

namespace StoreOnLine.DataBase.Concrete
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly StoreOnLineContext _context = new StoreOnLineContext();

        public IEnumerable<Supplier> Suppliers
        {
            get
            {
                return _context.Suppliers
                    .Where(o => !o.IsDeleted);
            }
        }
        public int SaveSupplier(Supplier supplier)
        {
            if (supplier.Id == 0)
            {
                _context.Suppliers.Add(supplier);
            }
            else
            {
                var dbEntry = _context.Suppliers.Find((supplier.Id));
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(supplier))
                {
                    if (!property.Name.Equals("Id") && property.GetValue(supplier) != null)
                    {
                        var value = property.GetValue(supplier);
                        if (value != null) property.SetValue(dbEntry, value);
                    }
                }

            }
            _context.SaveChanges();
            return supplier.Id;
        }

        public Supplier DeleteSupplier(int supplierId, bool physical = false)
        {
            var dbEntry = _context.Suppliers.Find(supplierId);
            if (dbEntry == null) return null;
            if (physical)
            {
                _context.Suppliers.Remove(dbEntry);
            }
            else
            {
                dbEntry.IsDeleted = true;
                dbEntry.Active = false;
                _context.Entry(dbEntry).State = EntityState.Modified;
            }

            _context.SaveChanges();
            return dbEntry;
        }
    }
}
