using System.Data.Entity;
using StoreOnLine.DataBase.CMS;
using StoreOnLine.Service.Business;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreOnLine.Service.Services
{
    public class PageService : BaseService
    {
        private const string Cacheid = "page-{0}";

        readonly LocalizePropertyService _propertyservice = new LocalizePropertyService();
        readonly LogService _log = new LogService();

        public IEnumerable<Page> GetPageBySiteId()
        {
            return Db.Pages.ToList();
        }

        public IEnumerable<Page> GetChildPageByPageId(int parentId)
        {
            Key = string.Format(Cacheid, "child" + parentId);
            return Cache.Get(Key, CacheInMinute, () => Db.Pages.Where(c => c.ParentId == parentId).ToList());
        }

        public Page GetPageById(int id)
        {
            Key = string.Format(Cacheid, id);
            return Cache.Get(Key, CacheInMinute, () => Db.Pages.SingleOrDefault(c => c.Id == id));
        }

        //Menu
        //Head Menu
        public IOrderedQueryable<Page> GetPageSiteListParent(int parentId)
        {
            Key = Cacheid;
            return Cache.Get(Key, CacheInMinute, () => from p in Db.Pages
                                                       where p.ParentId == parentId && p.IncludeInMenu
                                                       orderby p.PageOrder
                                                       ascending
                                                       select p);
        }
        //Sub Head Menu
        public IOrderedQueryable<Page> GetPageSiteListChild(int parentId)
        {
            Key = string.Format(Cacheid, "submenu-site" + parentId);
            return Cache.Get(Key, CacheInMinute, () => from p in Db.Pages
                                                       where p.ParentId == parentId && p.IncludeInMenu
                                                       orderby p.PageOrder
                                                       ascending
                                                       select p);
        }
        //End Menu
        public bool InsertPage(Page page, string seoValue)
        {
            try
            {
                Db.Pages.Add(page);
                Db.SaveChanges();
                _propertyservice.pageName_Proerties_Add(page.Id, page.PageName, seoValue);
                return true;
            }
            catch (Exception ex)
            {
                _log.InsertLog("page-insert", ex.ToString());
                return false;
            }
        }
        public bool UpdatePage(Page page, string seoValue)
        {
            try
            {
                Db.Entry(page).State = EntityState.Modified;
                Db.SaveChanges();
                _propertyservice.pageName_Proerties_Update(page.Id, page.PageName, seoValue);
                return true;
            }
            catch (Exception ex)
            {
                _log.InsertLog("page-update", ex.ToString());
                return false;
            }
        }

        public bool DeletePageById(int id)
        {
            try
            {
                var p = Db.Pages.Find(id);
                Db.Pages.Remove(p);
                Db.SaveChanges();
                _propertyservice.pageName_Proerties_Delete(p.Id);
                return true;
            }
            catch (Exception ex)
            {
                _log.InsertLog("page-delete", ex.ToString());
                return false;
            }
        }

        public bool DeletePage(Page page)
        {
            try
            {
                var e = Db.Pages.SingleOrDefault(c => c.Id == page.Id);
                Db.Pages.Remove(e);
                Db.SaveChanges();
                _propertyservice.pageName_Proerties_Delete(page.Id);
                return true;
            }
            catch (Exception ex)
            {
                _log.InsertLog("page-delete", ex.ToString());
                return false;
            }
        }
    }
}
