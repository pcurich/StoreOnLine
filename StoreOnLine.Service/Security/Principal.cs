using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.Service.Security
{
    public class Principal : IPrincipal
    {
        readonly StoreOnLineContext _db = new StoreOnLineContext();

        public IIdentity Identity { get; private set; }

        public Principal(IIdentity identity)
        {
            Identity = identity;
        }

        public bool IsInRole(string role)
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                return false;
            }

            var rol = _db.RolesCms.FirstOrDefault(o => o.RoleName == role);
            var user = _db.UsersCms.FirstOrDefault(o => o.UserName == HttpContext.Current.User.Identity.Name);

            var userInRol = _db.UserInRoles.FirstOrDefault(c => c.UserId == user.Id && c.RolId == rol.Id);
            
            return userInRol != null;
        }

    }
}
