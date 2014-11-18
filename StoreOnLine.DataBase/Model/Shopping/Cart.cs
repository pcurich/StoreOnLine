using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.CmsProduct;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Model.Shopping
{
    public class Cart:DataBaseId
    {
        public List<CartLine> LineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            CartLine line = LineCollection.FirstOrDefault(p => p.Product.Id == product.Id);
            if (line == null)
            {
                LineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
            LineCollection.RemoveAll(l => l.Product.Id == product.Id);
        }

        public decimal ComputeTotalValue()
        {
            return LineCollection.Sum(e => e.SubTotal);
        }
        public void Clear()
        {
            LineCollection.Clear();
        }
        public IEnumerable<CartLine> Lines
        {
            get { return LineCollection; }
        }
    }
}
