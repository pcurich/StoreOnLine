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

        public IEnumerable<UserInRoles> GetUserInRoles()
        {
            return Db.UserInRoles.ToList();
        }

        public IEnumerable<UserInRoles> GetByRoleName(string roleName)
        {
            if (roleName == null || "".Equals(roleName)) 
                return null;

            var userInRol = (from uir in Db.UserInRoles
                join r in Db.RolesCms on uir.RolId equals r.Id
                where r.RoleName == roleName
                select uir).ToList();

            return userInRol;
        }

        public IEnumerable<UserInRoles> GetByUserName(string userName)
        {
            if (userName == null || "".Equals(userName))
                return null;

            var userInRol = (from uir in Db.UserInRoles
                             join u in Db.UsersCms on uir.UserId equals u.Id
                             where u.UserName == userName
                             select uir).ToList();

            return userInRol;
        }

        public int Add(UserInRoles e)
        {
            Db.UserInRoles.Add(e);
            return Db.SaveChanges();
        }

        private int Update(UserInRoles e)
        {
            Db.Entry(e).State = EntityState.Modified;
            return Db.SaveChanges();
        }

        public int Delete(UserInRoles e, bool physical = false)
        {
            if (physical)
            {
                var l = Db.UserInRoles.SingleOrDefault(p => p.RolId == e.RolId && p.UserId == e.UserId);
                Db.UserInRoles.Remove(l);
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
