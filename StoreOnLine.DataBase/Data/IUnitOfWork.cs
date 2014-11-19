using StoreOnLine.DataBase.Data.ICmsCategory;
using StoreOnLine.DataBase.Data.ICmsEmployer;
using StoreOnLine.DataBase.Data.ICmsRol;
using StoreOnLine.DataBase.Model.CmsCategory;
using StoreOnLine.DataBase.Model.CmsRol;

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

        #region Repository
        
        #region Category
        ICategory CategoryRepository { get; }
        ICategoryLang CategoryLangRepository { get; }
        ICategoryShop CategoryShopRepository { get; }
        IRepository<CategoryRol> CategoryRolRepository { get; }
        IRepository<CategoryProduct> CategoryProductRepository { get; }//todo
        #endregion

        #region Rol
        IRol RolRepository { get; }
        #endregion
        IEmployer EmployerRepository { get; }
        IEmployerShop EmployerShopRepository { get; }
        #endregion
    }
}