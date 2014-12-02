using System.Linq;
using StoreOnLine.DataBase.Model.CmsProduct;
using System;
using System.Data.Entity;

namespace StoreOnLine.DataBase.Data.ICmsProduct
{
    public class RepoProduct : StoreRepository<Product>, IProduct
    {
        public RepoProduct(DbContext dbContext, string user) : base(dbContext, user) { }

        public Product GetProductByProductLang(int productLangId)
        {
            return GetAll().First(o => o.ProductLangId == productLangId);
        }
    }
}
