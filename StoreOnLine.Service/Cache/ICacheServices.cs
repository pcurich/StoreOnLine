using System;

namespace StoreOnLine.Service.Cache
{
    public interface ICacheService
    {
        T Get<T>(string cacheId, int duration, Func<T> getItemCallback) where T : class;
    }
}
