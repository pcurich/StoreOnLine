using System.ComponentModel.DataAnnotations;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.CMS
{
    /// <summary>
    /// todo
    /// </summary>
    public class SiteSetting:DataBaseId
    {
        [Key]
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
       // [UIHint("DbTypeDropDown")]
        public string DbType { get; set; }
        public string Description { get; set; }
    }
}
