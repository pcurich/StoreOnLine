using System;
using System.Collections.Generic;
using System.Web;

namespace StoreOnLine.Service.Cache
{
    public class CacheMemory : ICacheService
    {
        public T Get<T>(string cacheId, int duration, Func<T> getItemCallback) where T : class
        {
            var item = HttpRuntime.Cache.Get(cacheId) as T;

            if (item != null)
                return item;

            item = getItemCallback();
            HttpRuntime.Cache.Insert(cacheId, item, null,
                                     DateTime.Now.AddMinutes(duration),
                                     System.Web.Caching.Cache.NoSlidingExpiration);

            return item;
        }

        public void Remove(string cacheId)
        {
            HttpRuntime.Cache.Remove(cacheId);
        }

        public void Clear()
        {
            var enumerator = HttpRuntime.Cache.GetEnumerator();
            var cacheItems = new Dictionary<string, object>();

            while (enumerator.MoveNext())
            {
                cacheItems.Add(enumerator.Key.ToString(), enumerator.Value);
            }

            foreach (var key in cacheItems.Keys)
            {
                HttpRuntime.Cache.Remove(key);
            }

            ////modify web.config to remove cache
            var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            var setting = config.AppSettings.Settings["RefreshCache"];

            config.AppSettings.Settings["RefreshCache"].Value = setting.Value == "1" ? "0" : "1";
            config.Save();

            //RestartApplication.Restart();// se utiliza para reiniciar el CMS y borrar las caches
            // ResourceString.Clear();
            // PropertyString.Clear();
            //SiteSettingString.Clear();
            //ModuleSettingString.Clear();
            // PageRoute.Clear();
            // SeoUrl.Clear();

        }
    }
}
