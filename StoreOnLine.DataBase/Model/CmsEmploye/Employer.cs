using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.CmsLanguage;
using StoreOnLine.DataBase.Model.CmsRol;

namespace StoreOnLine.DataBase.Model.CmsEmploye
{
    public class Employer:DataBaseId
    {
        public Rol Rol { get; set; }
        public int RolId { get; set; }

        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime LastPassWordGenered { get; set; }

    }
}
