﻿using System.Data.Entity;
using System.Linq;
using StoreOnLine.DataBase.Model.CmsCategory;

namespace StoreOnLine.DataBase.Data.ICmsCategory
{
    public class RepoCategoryLang:StoreRepository<CategoryLang>,ICategoryLang
    {
        public RepoCategoryLang(DbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<CategoryLang> GetCategoryLangForCultura(int languageId)
        {
            return GetAll().Where(o => o.LanguageId == languageId);
        }
    }
}
