using System;
using System.Collections.Generic;
using System.Web;

namespace StoreOnLine.Service
{
    public class CacheMemory : ICacheService
    {
        public T Get<T>(string cacheId, int duration, Func<T> getItemCallback) where T : class
        {
            var item = HttpRuntime.Cache.Get(cacheId) as T;
            if (item != null) return item;
            item = getItemCallback();
            HttpRuntime.Cache.Insert(cacheId, item, null, DateTime.Now.AddMinutes(duration), System.Web.Caching.Cache.NoSlidingExpiration);

            return item;
        }

        public void Remove(string cacheId)
        {
            HttpRuntime.Cache.Remove(cacheId);
        }

        public void Clear()
        {
            var enumerator = HttpRuntime.Cache.GetEnumerator();
            Dictionary<string, object> cacheItems = new Dictionary<string, object>();

            while (enumerator.MoveNext())
            {
                cacheItems.Add(enumerator.Key.ToString(), enumerator.Value);
            }

            foreach (string key in cacheItems.Keys)
            {

                HttpRuntime.Cache.Remove(key);
            }

            System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");

            config.AppSettings.Settings["RefreshCache"];

            RestartApplication.Restart();// se utiliza para reiniciar el CMS y borrar las caches
            ResourceString.Clear();

            PropertyString.Clear();

            SiteSettingString.Clear();

            ModuleSettingString.Clear();

            PageRoute.Clear();

            SeoUrl.Clear();

        }
    }

    //Es utilizada como cache de data para rapido acceso 
}
