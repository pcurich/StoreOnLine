﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Model.Shopping;

namespace StoreOnLine.DataBase.Concrete
{
    public static class CartRepository
    {
        public static void AddItem(this Cart cart, Product product, int quantity)
        {
            throw new NotImplementedException();
        }

        public static void RemoveLine(Product product)
        {
            throw new NotImplementedException();
        }

        public static decimal ComputeTotalValue()
        {
            throw new NotImplementedException();
        }

        public static void Clear()
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<CartLine> Lines()
        {
            throw new NotImplementedException();
        }
    }
}
