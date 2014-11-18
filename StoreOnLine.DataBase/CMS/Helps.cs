using System.ComponentModel.DataAnnotations;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model;

namespace StoreOnLine.DataBase.CMS
{
    /// <summary>
    /// todo
    /// </summary>
    public class Helps : DataBaseId
    {
        public int SiteSettingId { get; set; }
        public SiteSetting SiteSetting { get; set; }
        public string Description { get; set; }
    }
}

