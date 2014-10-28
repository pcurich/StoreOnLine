using System.ComponentModel.DataAnnotations;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.CMS
{
    public class SiteSettings:DataBaseId
    {
        [Key]
        //[Column(Order = 1)]
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
       // [UIHint("DbTypeDropDown")]
        public string DbType { get; set; }
        public string Description { get; set; }
    }
}
