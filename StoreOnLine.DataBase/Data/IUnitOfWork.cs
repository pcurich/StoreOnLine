using StoreOnLine.DataBase.Data.ICmsCategory;
using StoreOnLine.DataBase.Data.ICmsEmployer;
using StoreOnLine.DataBase.Data.ICmsLanguage;
using StoreOnLine.DataBase.Data.ICmsProduct;
using StoreOnLine.DataBase.Data.ICmsRol;
using StoreOnLine.DataBase.Model.CmsCategory;

namespace StoreOnLine.DataBase.Data
{
    /// <summary>
    ///     Interface for the My Unit of Work"
    /// </summary>
    public interface IUnitOfWork
    {
        // Save pending changes to the data store.
        void Commit();

        void SetCurrentUser(string user);

        void SetCurrentCulture(int cultureId);

        #region Repository

        #region Category
        ICategory CategoryRepository { get; }
        ICategoryLang CategoryLangRepository { get; }
        ICategoryShop CategoryShopRepository { get; }
        IRepository<CategoryRol> CategoryRolRepository { get; }
        IRepository<CategoryProduct> CategoryProductRepository { get; }//todo
        #endregion

        #region Products
        IProduct ProductRepository { get; }
        IProductLang ProductLangRepository { get; }
        #endregion

        #region Rol
        IRol RolRepository { get; }
        #endregion

        #region Employer
        IEmployer EmployerRepository { get; }
        IEmployerShop EmployerShopRepository { get; }
        #endregion

        #region language
        ILanguage LanguageRepository { get; }
        #endregion

        #endregion
    }
}