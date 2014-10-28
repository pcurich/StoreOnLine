using System;

namespace StoreOnLine.Service.Business
{
    public interface ICacheService
    {
        T Get<T>(string cacheId, int duration, Func<T> getItemCallback) where T : class;
    }
}
