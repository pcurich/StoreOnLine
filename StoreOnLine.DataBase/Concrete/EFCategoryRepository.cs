using System.Linq;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.CmsCategory;
using StoreOnLine.DataBase.Model.Products;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;

namespace StoreOnLine.DataBase.Concrete
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly StoreOnLineContext _context = new StoreOnLineContext();

        public IEnumerable<Category> Categories
        {
            get
            {
                return _context.Categories
                    .Where(o=>!o.IsDeleted);
            }
        }

        public int SaveCategory(Category category)
        {
            if (category.Id == 0)
            {
                _context.Categories.Add(category);
            }
            else
            {
                var dbEntry = _context.Categories.Find((category.Id));
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(category))
                {
                    if (!property.Name.Equals("Id") && property.GetValue(category) != null)
                    {
                        var value = property.GetValue(category);
                        if (value != null) property.SetValue(dbEntry, value);
                    }
                }

            }
            _context.SaveChanges();
            return category.Id;
        }

        public Category DeleteCategory(int categoryId, bool physical=false)
        {
            var dbEntry = _context.Categories.Find(categoryId);
            if (dbEntry != null)
            {
                if (physical)
                {
                    _context.Categories.Remove(dbEntry);
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
