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

        public IEnumerable<User> GetUsersBySiteId()
        {
            return Db.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return Db.Users.Find(id);
        }

        public void Add(User e)
        {
            Db.Users.Add(e);
            Db.SaveChanges();
        }

        public void Update(User e)
        {
            Db.Entry(e).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void Delete(User e, bool physical = false)
        {
            if (physical)
            {
                var l = Db.Users.SingleOrDefault(p => p.Id == e.Id);
                Db.Users.Remove(l);
                Db.SaveChanges();
            }
            else
            {
                e.IsDeleted = true;
                Update(e);
            }
        }

        public void ClearCache()
        {
            Cache.Remove(Cacheid);
        }

    }
}
