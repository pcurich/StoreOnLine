using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Concrete
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly StoreOnLineContext _context = new StoreOnLineContext();

        public IEnumerable<Category> Categories { get { return _context.Categories; } }
    }
}
