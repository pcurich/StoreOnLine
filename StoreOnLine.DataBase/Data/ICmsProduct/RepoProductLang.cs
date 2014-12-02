using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Model.CmsProduct;

namespace StoreOnLine.DataBase.Data.ICmsProduct
{
    public class RepoProductLang : StoreRepository<ProductLang>, IProductLang
    {
        public RepoProductLang(DbContext dbContext, string user) : base(dbContext, user){}

        public IQueryable<ProductLang> GetProductLangForCultura(int languageId)
        {
            return GetAll().Where(o => o.LanguageId == languageId);
        }
    }
}
