using StoreOnLine.DataBase.CMS;
using StoreOnLine.Service.Business;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StoreOnLine.Service.Services
{
    public class SiteSettingService : BaseService
    {
        private const string Cacheid = "SiteSettings";

        public IEnumerable<SiteSetting> GetSiteSetting()
        {
            return Db.SiteSettings.ToList();
        }

        public int Add(SiteSetting e)
        {
            Db.SiteSettings.Add(e);
            return Db.SaveChanges();
        }

        public void UpdateSiteSetting(SiteSetting e)
        {
            Db.Entry(e).State = EntityState.Modified;
            Db.SaveChanges();
            SiteSettingString.UpdateKey(e.SettingKey, e.SettingValue);
        }
        public void Updateinline(string id, string value)
        {
            var e = Db.SiteSettings.SingleOrDefault(p => p.SettingKey == id);
            
            if (e == null) 
                return;

            e.SettingKey = id;
            e.SettingValue = value;
            Db.SaveChanges();
            SiteSettingString.UpdateKey(e.SettingKey, e.SettingValue);
        }
        public void Delete(SiteSetting e, bool physical = false)
        {
            if (physical)
            {
                var l = Db.SiteSettings.SingleOrDefault(p => p.SettingKey == e.SettingKey);
                Db.SiteSettings.Remove(l);
                Db.SaveChanges();
                SiteSettingString.RemoveKey(e.SettingKey);
            }
            else
            {
                e.IsDeleted = true;
                UpdateSiteSetting(e);
            }
        }

        public void ClearCache()
        {
            Cache.Remove(Cacheid);
        }
    }
}
