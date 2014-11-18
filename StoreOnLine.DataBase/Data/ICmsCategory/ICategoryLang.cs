using System.Linq;
using StoreOnLine.DataBase.Model.CmsCategory;

namespace StoreOnLine.DataBase.Data.ICmsCategory
{
    public interface ICategoryLang : IRepository<CategoryLang>
    {
        //gets the categories for the same Culture
        IQueryable<CategoryLang> GetCategoryLangForCultura(int languageId);
    }
}