using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.Service.Business
{
    public class BaseService
    {
        public CacheMemory Cache = new CacheMemory();
        public StoreOnLineContext Db = new StoreOnLineContext();
        public int CacheInMinute = 60;//Convert.ToInt16(SiteSettingString.GetString("CacheInMinute")); 
        public string Key = string.Empty;
    }

    
}
