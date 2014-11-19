using System.Data.Entity;
using System.Linq;
using StoreOnLine.DataBase.Model.CmsCategory;

namespace StoreOnLine.DataBase.Data.ICmsCategory
{
    public class RepoCategoryShop : StoreRepository<CategoryShop>, ICategoryShop
    {
        public RepoCategoryShop(DbContext dbContext, string user)
            : base(dbContext,user)
        {
        }

        public CategoryShop GetCategoryByShop(int shopId)
        {
            return GetAll().First(o => o.ShopId == shopId);
        }
    }
}