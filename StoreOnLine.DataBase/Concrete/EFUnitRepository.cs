using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Concrete
{
    public class UnitRepository:IUnitRepository
    {
        private readonly StoreOnLineContext _context = new StoreOnLineContext();

        public IEnumerable<Unit> Units { get { return _context.Units.Where(o => !o.IsDeleted); } }
        
        public int SaveUnit(Unit unit)
        {
            if (unit.Id == 0)
            {
                _context.Units.Add(unit);
            }
            else
            {
                var dbEntry = _context.Units.Find((unit.Id));
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(unit))
                {
                    if (!property.Name.Equals("Id") && property.GetValue(unit) != null)
                    {
                        var value = property.GetValue(unit);
                        if (value != null) property.SetValue(dbEntry, value);
                    }
                }

            }
            _context.SaveChanges();
            return unit.Id;
        }

        public Unit DeleteUnit(int unitId, bool physical = false)
        {
            var dbEntry = _context.Units.Find(unitId);
            if (dbEntry != null)
            {
                if (physical)
                {
                    _context.Units.Remove(dbEntry);
                }
                else
                {
                    dbEntry.IsDeleted = true;
                    dbEntry.Active = false;
                    _context.Entry(dbEntry).State = EntityState.Modified;
                }

                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
