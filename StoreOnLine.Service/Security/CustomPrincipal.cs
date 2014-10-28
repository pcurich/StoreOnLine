using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.Service.Security
{
    public class CustomPrincipal : IPrincipal
    {
        readonly StoreOnLineContext _db = new StoreOnLineContext();

        public CustomPrincipal(CustomIdentity identity)
        {
            Identity = identity;
        }

        #region IPrincipal Members

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                return false;
            }
            var roles = _db.UserInRoles.Where(c => c.UserName == HttpContext.Current.User.Identity.Name);

            return Enumerable.Any(roles, r => String.Equals(r.RoleName, role, StringComparison.CurrentCultureIgnoreCase));
        }

        #endregion

    }
}
