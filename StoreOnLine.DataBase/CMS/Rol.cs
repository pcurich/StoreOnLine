using System.Collections.Generic;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model;

namespace StoreOnLine.DataBase.CMS
{
    /// <summary>
    /// Rol disponible en el sistema
    /// Pueden ser :
    ///         User
    ///         Administrator
    ///         Content 
    ///         Publisher     
    /// </summary>
    public class Rol : DataBaseId
    {
        public string RoleName { get; set; }
        public List<UserInRoles> UserInRoleses { get; set; }
    }

}
