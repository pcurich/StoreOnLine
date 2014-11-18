using StoreOnLine.DataBase.Model.CmsCategory;

namespace StoreOnLine.DataBase.Data.ICmsCategory
{
    public interface ICategoryShop : IRepository<CategoryShop>
    {
        CategoryShop GetCategoryByShop(int shopId);
    }
}