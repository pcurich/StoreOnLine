using System.Linq;
using StoreOnLine.DataBase.CMS;
using StoreOnLine.Service.Business;
using System;
using System.Collections.Generic;
using System.Web;

namespace StoreOnLine.Service.Services
{
    public static class Logwriter
    {
        public static void Insert(string logtype, string description)
        {
            var ls = new LogService();
            ls.InsertLog(logtype, description);
        }
    }

    public class LogService : BaseService
    {
        public void InsertLog(string logType, string descriprion)
        {
            var log = new Log
            {
                LogType = logType,
                LogDescription = descriprion,
                UserName = HttpContext.Current == null ? "system" : HttpContext.Current.User.Identity.Name,
                LoggedDate = DateTime.Now
            };
            Db.Logs.Add(log);
            Db.SaveChanges();
        }
        public IEnumerable<Log> GetLogBySiteId(string siteId)
        {
            return Db.Logs.ToList();
        }
        public Log GetLogById(int id)
        {
            return Db.Logs.Find(id);
        }
        public Log GetLogLatest()
        {
            return Db.Logs.Find(Db.Logs.Max(c => c.Id));
        }
        public void DeleteById(int id)
        {
            var p = Db.Logs.Find(id);
            Db.Logs.Remove(p);
            Db.SaveChanges();
        }
        public void DeleteAll()
        {
            var log = Db.Logs.ToList();
            foreach (var l in log.Select(item => Db.Logs.Find(item.Id)))
            {
                Db.Logs.Remove(l);
            }
            Db.SaveChanges();
        }
    }
}
