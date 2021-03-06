﻿using System;
using System.Data.Entity;
using System.Linq;
using StoreOnLine.DataBase.Model.CmsCategory;

namespace StoreOnLine.DataBase.Data.ICmsCategory
{
    public class RepoCategory : StoreRepository<Category>, ICategory
    {
        public RepoCategory(DbContext dbContext,string user )
            : base(dbContext,user)
        {
        }

        public Category GetCategoryByCategoryLang(int categoryLangId)
        {
            return GetAll().First(o => o.CategoryLangId == categoryLangId);
        }
    }
}