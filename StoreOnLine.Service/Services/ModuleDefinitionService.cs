using StoreOnLine.DataBase.CMS;
using StoreOnLine.Service.Business;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StoreOnLine.Service.Services
{
    public class ModuleDefinitionService : BaseService
    {
        private const string Cacheid = "ModuleDefinitions";

        public IEnumerable<ModuleDefinitions> GetModuleDefinitionList()
        {
            return Db.ModuleDefinitions.ToList();
        }

        public void Add(ModuleDefinitions e)
        {
            Db.ModuleDefinitions.Add(e);
            Db.SaveChanges();
        }

        public void Update(ModuleDefinitions e)
        {
            Db.Entry(e).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void Delete(ModuleDefinitions e)
        {
            var l = Db.ModuleDefinitions.SingleOrDefault(p => p.Id == e.Id);
            Db.ModuleDefinitions.Remove(l);
            Db.SaveChanges();
        }

        public void ClearCache()
        {
           Cache.Remove(Cacheid);
        }
    }
}
