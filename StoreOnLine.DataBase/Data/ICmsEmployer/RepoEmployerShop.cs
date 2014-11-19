using System.Data.Entity;
using System.Linq;
using StoreOnLine.DataBase.Model.CmsEmploye;

namespace StoreOnLine.DataBase.Data.ICmsEmployer
{
    public class RepoEmployerShop : StoreRepository<EmployerShop>, IEmployerShop
    {
        public RepoEmployerShop(DbContext dbContext, string user)
            : base(dbContext,user)
        {
        }

        public EmployerShop GetCurrentEmployherByShop(int shopId)
        {
            return GetAll().First(o => o.ShopId == shopId);
        }
    }
}