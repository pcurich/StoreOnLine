using StoreOnLine.DataBase.Data.ICmsCategory;
using StoreOnLine.DataBase.Model.CmsCategory;

namespace StoreOnLine.DataBase.Data
{
    /// <summary>
    /// Interface for the My Unit of Work"
    /// </summary>
    public interface IUnitOfWork
    {
        // Save pending changes to the data store.
        void Commit();

        //Repositories
        ICategory CategoryRepository { get; }
        ICategoryLang CategoryLangRepository { get; }
        ICategoryShop CategoryShopRepository { get; }
        IRepository<CategoryProduct> CategoryProductRepository { get; }
    }
}
