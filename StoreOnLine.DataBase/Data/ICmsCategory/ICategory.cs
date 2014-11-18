using StoreOnLine.DataBase.Model.CmsCategory;

namespace StoreOnLine.DataBase.Data.ICmsCategory
{
    public interface ICategory : IRepository<Category>
    {
        Category GetCategoryByCategoryLang(int categoryLangId);
    }
}