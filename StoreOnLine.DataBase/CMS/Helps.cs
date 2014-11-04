using System.ComponentModel.DataAnnotations;
using StoreOnLine.DataBase.Entities;

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

