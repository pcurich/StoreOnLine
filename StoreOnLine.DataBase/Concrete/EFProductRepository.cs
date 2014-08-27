using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Concrete
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly StoreOnLineContext _context = new StoreOnLineContext();

        public IEnumerable<ProductBase> ProductBases
        {
            get
            {
                return _context.ProductBases
                    .Include(o => o.ProductCategory)
                    .Include(o => o.ProductCampaign)
                    .Include(o => o.ProductUnit);
            }
        }

        public IEnumerable<ProductComposite> ProductComposites
        {
            get
            {
                return _context.ProductComposites
                .Include(o => o.ProductCategory)
                    .Include(o => o.ProductCampaign)
                    .Include(o => o.ProductUnit);
            }
        }

        public IEnumerable<Category> Categories
        {
            get { return _context.Categories; }
        }
        public IEnumerable<Campaign> Campaigns
        {
            get { return _context.Campaigns; }
        }

        public IEnumerable<Unit> Units
        {
            get { return _context.Units; }
        }
    }
}
