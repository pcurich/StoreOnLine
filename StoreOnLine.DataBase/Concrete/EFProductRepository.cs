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
    public class ProductsRepository : IProductsRepository
    {
        private readonly StoreOnLineContext _context = new StoreOnLineContext();
        public IEnumerable<Product> Products
        {
            get { return _context.ProductBases; }
        }

    }
}
