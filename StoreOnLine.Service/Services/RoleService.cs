using StoreOnLine.DataBase.CMS;
using StoreOnLine.Service.Business;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StoreOnLine.Service.Services
{
    public class RoleService : BaseService
    {
        private const string Cacheid = "Roles";

        public IEnumerable<Roles> GetRolesBySiteId()
        {
            return null;// Db.Roles.ToList();
        }

        public void Add(Roles e)
        {
            //Db.Roles.Add(e);
            Db.SaveChanges();
        }

        public void Update(Roles e)
        {
            Db.Entry(e).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void Delete(Roles e, bool physical = false)
        {
            if (physical)
            {
                var l = Db.Roles.SingleOrDefault(p => p.RoleName == e.RoleName);
                Db.Roles.Remove(l);
                Db.SaveChanges();
            }
            else
            {
                Update(e);
            }
        }

        public void ClearCache()
        {
            Cache.Remove(Cacheid);
        }
    }
}
