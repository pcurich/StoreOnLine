
namespace StoreOnLine.Service
{
    public class BaseService
    {
        public CacheMemory Cache = new CacheMemory();
        public CoreDbContext Db = new CoreDbContext();
        public int CacheInMinute = 60;//Convert.ToInt16(SiteSettingString.GetString("CacheInMinute")); 
        public string Key = string.Empty;
    }

    
}
