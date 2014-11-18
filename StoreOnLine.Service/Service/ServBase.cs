using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.Service.Cache;

namespace StoreOnLine.Service.Service
{
    public class ServBase
    {
        public CacheMemory Cache = new CacheMemory();
        public StoreOnLineContext Db = new StoreOnLineContext();
        public int CacheInMinute = 60;//Convert.ToInt16(SiteSettingString.GetString("CacheInMinute")); 
        public string Key = string.Empty;
    }
}
