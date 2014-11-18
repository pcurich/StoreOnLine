using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model;

namespace StoreOnLine.DataBase.CMS
{
    /// <summary>
    /// Permite establecer a que rol pertenece un usuario
    /// Un usuario puede estar en varios roles
    /// Un rol puede ser asignado a varios usuarios
    /// </summary>
    public class UserInRoles : DataBaseId
    {
        public User User { get; set; }
        public int UserId { get; set; }

        public Rol Rol { get; set; }
        public int RolId { get; set; }

        //[Key]
        //[Column(Order = 0)]
        //[Required]
        //public string UserName { get; set; }
        //[Key]
        //[Column(Order = 1)]
        //[Required]
        //public string RoleName { get; set; }
    }
}
