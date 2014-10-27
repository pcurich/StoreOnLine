using System.ComponentModel.DataAnnotations;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.CMS
{
    public class Roles : DataBaseId
    {
        [Key]
        [Required]
        public string RoleName { get; set; }
    }
    //Esto solo es para roles: User, Administrator, Content publisher 
}
