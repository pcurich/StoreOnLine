using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using StoreOnLine.DataBase.Model.CmsCategory;

namespace StoreOnLine.Service.Service.Categories
{
    /// <summary>
    /// Este Servicio se crea cuando se logea un usuario pues es en ese momento que se tiene la siguiente informacion
    /// </summary>
    public class ServCategory : ServBase
    {
        private static ServCategory _instance;
        private static readonly Object Mutex = new Object();

        readonly IDictionary<int, CategoryShop> _categoryShops = new Dictionary<int, CategoryShop>();
        readonly IDictionary<int, CategoryGroup> _categoryGroups = new Dictionary<int, CategoryGroup>();
        readonly IDictionary<int, Category> _categories = new Dictionary<int, Category>();
        readonly IDictionary<int, CategoryLang> _categoryLangs = new Dictionary<int, CategoryLang>();
        readonly IDictionary<int, CategoryProduct> _categoryProduct = new Dictionary<int, CategoryProduct>();

        private readonly int _shopId;
        private readonly int _languageId;

        public static ServCategory Instance(int shopId, int groupId)
        {
            if (_instance == null)
            {
                lock (Mutex)
                {
                    if (_instance == null)
                    {
                        _instance = new ServCategory(shopId, groupId);
                    }
                }
            }

            return _instance;
        }

        public ServCategory(int shopId, int languageId)
        {
            _languageId = languageId;
            _shopId = shopId;

            CategoryShop(shopId);
            Category();
            CategoryGroup(languageId);
            CategoryLang();
            CategoryProduct();
        }

        public CategoryLang GetNew()
        {
            return new CategoryLang();
        }

        public List<CategoryLang> GetAll()
        {
            return _categoryLangs.Values.ToList();
        }

        public CategoryLang GetOne(int categoryId)
        {
            return _categoryLangs[categoryId];
        }

        public int Update(CategoryLang categoryLang)
        {
            Db.CategoryLangs.Attach(categoryLang);
            var result = Db.SaveChanges();
            _categoryLangs[categoryLang.Id] = categoryLang;
            return result;
        }

        public int Create(CategoryLang categoryLang)
        {
            categoryLang.AddDate = Util.DateTime.DateTimeConvert.GetDateTimeNow();
            categoryLang.UpdDate = Util.DateTime.DateTimeConvert.GetDateTimeNow();
            categoryLang.LanguageId = _languageId;

            var newcategoryLang=Db.CategoryLangs.Add(categoryLang);
            var newId = Db.SaveChanges();

            var category = new Category { CategoryLangId = newId, UpdDate = Util.DateTime.DateTimeConvert.GetDateTimeNow(), AddDate = Util.DateTime.DateTimeConvert.GetDateTimeNow() };
            var newcategory =Db.Categories.Add(category);
            Db.SaveChanges();

            _categoryLangs.Add(newcategoryLang.Id, newcategoryLang);
            _categories.Add(newcategory.Id, newcategory);

            return 1;
        }

        public int Delete(CategoryLang categoryLang)
        {
            var result = Db.CategoryLangs.Remove(categoryLang);
            _categoryLangs.Remove(result.Id);
            return result.Id;
        }

        #region Internal

        internal void CategoryProduct()
        {
            var categoryProducs = (from cp in Db.CategoryProducts
                                   join c in Db.Categories on cp.CategoryId equals c.Id
                                   join cs in Db.CategoryShops.Where(o => o.ShopId == _shopId) on c.Id equals cs.CategoryId
                                   select cp).ToList();

            foreach (var categoryProduct in categoryProducs)
            {
                _categoryProduct.Add(categoryProduct.Id, categoryProduct);
            }
        }

        internal void CategoryLang()
        {
            var categoryLangs = (from cl in Db.CategoryLangs
                                 join l in Db.Languages on cl.LanguageId equals l.Id
                                 where l.Id == _languageId
                                 select cl).ToList();



            foreach (var categoryLang in categoryLangs)
            {
                _categoryLangs.Add(categoryLang.Id, categoryLang);
            }
        }

        internal void Category()
        {
            var categories = (from c in Db.Categories
                              join cs in Db.CategoryShops on c.Id equals cs.CategoryId
                              where cs.ShopId == _shopId
                              select c).ToList();

            foreach (var category in categories)
            {
                _categories.Add(category.Id, category);
            }
        }

        internal void CategoryShop(int shopId)
        {
            var categoryShops = (from c in Db.CategoryShops
                                 where c.ShopId == shopId
                                 select c);

            foreach (var categoryShop in categoryShops)
            {
                _categoryShops.Add(categoryShop.Id, categoryShop);
            }
        }

        internal void CategoryGroup(int groupId)
        {
            var categoryGroups = (from c in Db.CategoryGroups
                                  where c.GroupId == groupId
                                  select c);

            foreach (var categoryGroup in categoryGroups)
            {
                _categoryGroups.Add(categoryGroup.Id, categoryGroup);
            }
        }

        #endregion

    }


}
