using System.ComponentModel.DataAnnotations;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.CMS
{
    public class Helps : DataBaseId
    {
        [Key]
        public string SiteId { get; set; }
        [Key]
        public string Description { get; set; }
    }
}
