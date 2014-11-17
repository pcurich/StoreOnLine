using StoreOnLine.DataBase.CMS;
using StoreOnLine.Service.Business;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StoreOnLine.Service.Services
{
    public class LanguageService : BaseService
    {
        private const string Cacheid = "language-{0}";

        public IEnumerable<Language> GetLanguageBySiteId()
        {
            return null;//Db.Languages.ToList();
        }
        public IOrderedQueryable<Language> GetLanguagePublic()
        {
            //Key = Cacheid;
            //return Cache.Get(Key, CacheInMinute, () => from p in Db.Languages
            //                                           where p.Public
            //                                           orderby p.LanguageName
            //                                           ascending
            //                                           select p);
            return null;
        }
        public void AddLanguage(Language e)
        {
           // Db.Languages.Add(e);
            Db.SaveChanges();
        }
        public void UpdateLanguage(Language e)
        {
            Db.Entry(e).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void DeleteByKey(int id)
        {
            var l = Db.Languages.SingleOrDefault(p => p.Id == id);
            Db.Languages.Remove(l);
            Db.SaveChanges();
        }

        public void Delete(Language e)
        {
            var l = Db.Languages.SingleOrDefault(p => p.Id == e.Id);
            Db.Languages.Remove(l);
            Db.SaveChanges();
        }

        public void ClearCache()
        {
            Cache.Remove(Cacheid);
        }
    }
}
