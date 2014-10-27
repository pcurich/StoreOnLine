using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.CMS
{
    public class UserInRoles : DataBaseId
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public string UserName { get; set; }
        [Key]
        [Column(Order = 1)]
        [Required]
        public string RoleName { get; set; }
    }
    //Decir que rol le pertenece a un usuario
}
