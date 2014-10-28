using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StoreOnLine.DataBase.CMS;
using StoreOnLine.Service.Business;

namespace StoreOnLine.Service.Services
{
    public class PageModuleService : BaseService
    {
        private const string Cacheid = "pagemodule-{0}";

        readonly LocalizePropertyService _propertyservice = new LocalizePropertyService();
        readonly LogService _log = new LogService();

        public IEnumerable<PageModules> GetModuleByPageId(int pageId)
        {
            return Db.PageModules.Where(c => c.Id == pageId).ToList();
        }

        public PageModules FindPageModule(int id)
        {
            //var m = from p in Db.pagemodules join c in Db.moduledefinitions on p.ModuleDefId equals c.ModuleDefId where p.ModuleId == id select new { p, c.FeatureName };
            return Db.PageModules.Find(id);
        }

        public PageModules GetPageModuleById(int id)
        {
            //return cache.Get(key, cacheinminute, () => Db.pages.Where(c => c.PageId == id).SingleOrDefault());
            //return Db.pagemodules.Where(c => c.ModuleId == id).SingleOrDefault();
            return Cache.Get(Key, CacheInMinute, () => Db.PageModules.SingleOrDefault(c => c.Id == id));
        }

        public bool InsertPageModule(PageModules e)
        {
            try
            {
                e.CreatedByUser = HttpContext.Current.User.Identity.Name;
                e.CreatedDate = DateTime.UtcNow;
                Db.PageModules.Add(e);
                Db.SaveChanges();
                _propertyservice.moduleTitle_Proerties_Add(e.ModuleId, e.ModuleTitle);
                return true;
            }
            catch(Exception ex)
            {
                _log.InsertLog("pagemodule-insert", ex.ToString());
                return false;
            }
        }

        public bool UpdatePageModule(PageModules e)
        {
            try
            {
                Db.Entry(e).State = EntityState.Modified;
                Db.SaveChanges();
                _propertyservice.moduleTitle_Proerties_Update(e.ModuleId, e.ModuleTitle);
                return true;
            }
            catch (Exception ex)
            {
                _log.InsertLog("pagemodule-update", ex.ToString());
                return false;
            }
        }

        public bool DeletePageModuleById(int id)
        {
            try
            {
                var p = Db.PageModules.Find(id);
                Db.PageModules.Remove(p);
                Db.SaveChanges();
                _propertyservice.moduleTitle_Proerties_Delete(p.ModuleId);
                return true;
            }
            catch (Exception ex)
            {
                _log.InsertLog("pagemodule-delete", ex.ToString());
                return false;
            }
        }

        public bool DeletePageModule(PageModules e)
        {
            try
            {
                var p = Db.PageModules.SingleOrDefault(c => c.Id == e.Id);
                Db.PageModules.Remove(p);
                Db.SaveChanges();
                _propertyservice.moduleTitle_Proerties_Delete(p.ModuleId);
                return true;
            }
            catch (Exception ex)
            {
                _log.InsertLog("pagemodule-delete", ex.ToString());
                return false;
            }
        }
    }
    }
}
