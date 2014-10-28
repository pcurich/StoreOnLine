using StoreOnLine.DataBase.CMS;
using StoreOnLine.Service.Business;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StoreOnLine.Service.Services
{
    public class UserInRoleService : BaseService
    {
        private const string Cacheid = "Roles";

        public IEnumerable<UserInRoles> GetBySiteId()
        {
            return Db.UserInRoles.ToList();
        }

        public IEnumerable<UserInRoles> GetByRoleName(string roleName)
        {
            return string.IsNullOrEmpty(roleName) ? null : Db.UserInRoles.Where(c => c.RoleName == roleName).ToList();
        }

        public void Add(UserInRoles e)
        {
            Db.UserInRoles.Add(e);
            Db.SaveChanges();
        }

        private void Update(UserInRoles e)
        {
            Db.Entry(e).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void Delete(UserInRoles e, bool physical = false)
        {
            if (physical)
            {
                var l = Db.UserInRoles.SingleOrDefault(p => p.RoleName == e.RoleName && p.UserName == e.UserName);
                Db.UserInRoles.Remove(l);
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
