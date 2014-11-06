using System.Net.Configuration;
using StoreOnLine.DataBase.Model.Security;
using StoreOnLine.Service.Business;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StoreOnLine.Service.Services
{
    public class UserService : BaseService
    {
        private const string Cacheid = "Users-{0}";

        public IEnumerable<DataBase.CMS.User> GetUsers()
        {
            return Db.UsersCms.ToList();
        }

        public User GetUserById(int userId)
        {
            return Db.Users.Find(userId);
        }

        public int Add(DataBase.CMS.User e)
        {
            Db.UsersCms.Add(e);
            return Db.SaveChanges();
        }

        public int Update(DataBase.CMS.User e)
        {
            Db.Entry(e).State = EntityState.Modified;
            return Db.SaveChanges();
        }

        public int Delete(DataBase.CMS.User e, bool physical = false)
        {
            if (physical)
            {
                var l = Db.UsersCms.SingleOrDefault(p => p.Id == e.Id);
                Db.UsersCms.Remove(l);
                return Db.SaveChanges();
            }

            e.IsDeleted = true;
            return Update(e);
        }

        public void ClearCache()
        {
            Cache.Remove(Cacheid);
        }

    }
}
