using StoreOnLine.DataBase.Model.CmsProduct;
using System.Linq;

namespace StoreOnLine.DataBase.Data.ICmsProduct
{
    public interface IProductLang : IRepository<ProductLang>
    {
        //gets the Products for the same Culture
        IQueryable<ProductLang> GetProductLangForCultura(int languageId);
    }
}
