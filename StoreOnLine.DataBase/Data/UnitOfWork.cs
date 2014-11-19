using System;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Data.ICmsCategory;
using StoreOnLine.DataBase.Data.ICmsEmployer;
using StoreOnLine.DataBase.Data.ICmsRol;
using StoreOnLine.DataBase.Model.CmsCategory;

namespace StoreOnLine.DataBase.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private string _user;
        public UnitOfWork(string user)
        {
            _user = user;
            CreateDbContext();
        }

        #region Repositries
        
        #region Category
        private ICategory _categoriesRepository;
        private ICategoryLang _categoryLanRepository;
        private IRepository<CategoryRol> _categoryRolRepository; 
        private IRepository<CategoryProduct> _categoryProductRepository;
        private ICategoryShop _categoryShopRepository;

        //get Category repo
        public ICategory CategoryRepository
        {
            get { return _categoriesRepository ?? (_categoriesRepository = new RepoCategory(DbContext, _user)); }
        }

        //get CategoryLang repo
        public ICategoryLang CategoryLangRepository
        {
            get { return _categoryLanRepository ?? (_categoryLanRepository = new RepoCategoryLang(DbContext, _user)); }
        }

        //get CategoryShop repo
        public ICategoryShop CategoryShopRepository
        {
            get { return _categoryShopRepository ?? (_categoryShopRepository = new RepoCategoryShop(DbContext, _user)); }
        }

        public IRepository<CategoryRol> CategoryRolRepository
        {
            get{return _categoryRolRepository ??(_categoryRolRepository = new StoreRepository<CategoryRol>(DbContext, _user));}
        }

        //get CategoryProduct repo todo
        public IRepository<CategoryProduct> CategoryProductRepository
        {
            get{return _categoryProductRepository ??(_categoryProductRepository = new StoreRepository<CategoryProduct>(DbContext, _user));}
        }

        #endregion

        #region Rol
        private IRol _rolRepository;

        public IRol RolRepository
        {
            get { return _rolRepository ?? (_rolRepository = new RepoRol(DbContext, _user)); }
        }
        #endregion

        #region Employer

        private IEmployer _employerRepository;
        private IEmployerShop _employerShopRepository;

        public IEmployer EmployerRepository
        {
            get
            {
                return _employerRepository ??
                     (_employerRepository = new RepoEmployer(DbContext, _user));
            }
        }

        public IEmployerShop EmployerShopRepository
        {
            get
            {
                return _employerShopRepository ??
                     (_employerShopRepository = new RepoEmployerShop(DbContext, _user));
            }
        }

        #endregion

        #endregion

        private StoreOnLineContext DbContext { get; set; }

        //repositories

        /// <summary>
        ///     Save pending changes to the database
        /// </summary>
        public void Commit()
        {
            DbContext.SaveChanges();
        }

        protected void CreateDbContext()
        {
            DbContext = new StoreOnLineContext();

            // Do NOT enable proxied entities, else serialization fails.
            DbContext.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            DbContext.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            DbContext.Configuration.ValidateOnSaveEnabled = false;

            //DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (DbContext != null)
            {
                DbContext.Dispose();
            }
        }

        #endregion


        public void SetCurrentUser(string user)
        {
            _user = user;
        }
    }
}