using System.Data.Entity;
using System.Linq;
using StoreOnLine.DataBase.Model.CmsEmploye;

namespace StoreOnLine.DataBase.Data.ICmsEmployer
{
    public class RepoEmployerShop : StoreRepository<EmployerShop>, IEmployerShop
    {
        public RepoEmployerShop(DbContext dbContext) : base(dbContext)
        {
        }

        public EmployerShop GetCurrentEmployherByShop(int shopId)
        {
            return GetAll().First(o => o.ShopId == shopId);
        }
    }
}