using StoreOnLine.DataBase.CMS;
using StoreOnLine.Service.Business;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StoreOnLine.Service.Services
{
    public class ModuleSettingService : BaseService
    {
        public IEnumerable<ModuleSetting> GetModuleSettingByModuleId(int moduleId)
        {
            return Db.ModuleSettings.Where(c => c.Id == moduleId);
        }

        public void Add(int moduleId, string key, string value, string description)
        {
            var e = new ModuleSetting { Id = moduleId, SettingKey = Key, Value = value, Description = description };
            Db.ModuleSettings.Add(e);
            Db.SaveChanges();
        }

        public void Update(int moduleId, string key, string value)
        {
            var e = Db.ModuleSettings.SingleOrDefault(c => c.Id == moduleId && c.SettingKey == Key);
            if (e == null) 
                return;

            e.Value = value;
            Db.SaveChanges();
            ModuleSettingString.UpdateKey(moduleId.ToString(CultureInfo.InvariantCulture), Key, value);
        }

        public void Delete(int moduleId)
        {
            var l = Db.ModuleSettings.Where(c => c.Id == moduleId);
            foreach (var item in l)
            {
                var m = Db.ModuleSettings.SingleOrDefault(c => c.Id == item.Id && c.SettingKey == item.SettingKey);
                Db.ModuleSettings.Remove(m);
            }
            Db.SaveChanges();
        }
    }
}
