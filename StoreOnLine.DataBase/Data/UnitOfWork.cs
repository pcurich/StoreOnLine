﻿using System;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Data.ICmsCategory;
using StoreOnLine.DataBase.Model.CmsCategory;

namespace StoreOnLine.DataBase.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private StoreOnLineContext DbContext { get; set; }

        public UnitOfWork()
        {
            CreateDbContext();
        }

        //repositories
        #region Repositries

        private ICategory _categoriesRepository;
        private ICategoryLang _categoryLanRepository;
        private ICategoryShop _categoryShopRepository;
        private IRepository<CategoryProduct> _categoryProductRepository;

        //get Category repo
        public ICategory CategoryRepository
        {
            get { return _categoriesRepository ?? (_categoriesRepository = new RepoCategory(DbContext)); }
        }

        //get CategoryLang repo
        public ICategoryLang CategoryLangRepository
        {
            get { return _categoryLanRepository ?? (_categoryLanRepository = new RepoCategoryLang(DbContext)); }
        }

        //get CategoryShop repo
        public ICategoryShop CategoryShopRepository
        {
            get { return _categoryShopRepository ?? (_categoryShopRepository = new RepoCategoryShop(DbContext)); }
        }

        //get CategoryProduct repo
        public IRepository<CategoryProduct> CategoryProductRepository
        {
            get { return _categoryProductRepository ?? (_categoryProductRepository = new StoreRepository<CategoryProduct>(DbContext)); }
        }

        #endregion

        /// <summary>
        /// Save pending changes to the database
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

    }
}
