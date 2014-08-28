using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                    .Include(o => o.ProductImagens)
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
                    .Include(o => o.ProductImagens)
                    .Include(o => o.ProductCategory)
                    .Include(o => o.ProductCampaign)
                    .Include(o => o.ProductUnit);
            }
        }

        public int SaveProductBase(ProductBase product)
        {
            if (product.Id == 0)
            {
                _context.ProductBases.Add(product);

            }
            else
            {
                var dbEntry = _context.ProductBases.Find((product.Id));
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(product))
                {
                    if (!property.Name.Equals("Id") && property.GetValue(product) != null)
                    {
                        var value = property.GetValue(product);
                        if (value != null) property.SetValue(dbEntry, value);
                    }
                }

            }
            _context.SaveChanges();
            return product.Id;
        }

        public int SaveProductComposite(ProductComposite product)
        {
            if (product.Id == 0)
            {
                _context.ProductComposites.Add(product);

            }
            else
            {
                var dbEntry = _context.ProductComposites.Find((product.Id));
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(product))
                {
                    if (!property.Name.Equals("Id") && property.GetValue(product) != null)
                    {
                        var value = property.GetValue(product);
                        if (value != null) property.SetValue(dbEntry, value);
                    }
                }

            }
            _context.SaveChanges();
            return product.Id;
        }

        public Product DeleteProductBase(int productId)
        {
            ProductBase dbEntry = _context.ProductBases.Find(productId);
            if (dbEntry != null)
            {
                _context.ProductBases.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public Product DeleteProductComposite(int productId)
        {
            ProductComposite dbEntry = _context.ProductComposites.Find(productId);
            if (dbEntry != null)
            {
                _context.ProductComposites.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
