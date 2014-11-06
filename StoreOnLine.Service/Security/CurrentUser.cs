using System.Linq;
using System.Web;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.Service.Services;

namespace StoreOnLine.Service.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrentUser
    {
        static readonly StoreOnLineContext Db = new StoreOnLineContext();
        static PageModuleService _pms = new PageModuleService();
        static PageService _ps = new PageService();

        public static bool IsInRole(string role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }

        public static int GetUserId()
        {
            var user = Db.UsersCms.SingleOrDefault(c => c.UserName == HttpContext.Current.User.Identity.Name);

            if (user != null)
            {
                return user.Id;
            }
            return -1;
        }

        public static string[] GetRoles()
        {
            var user = Db.UsersCms.FirstOrDefault(o => o.UserName == HttpContext.Current.User.Identity.Name);

            if (user == null)
                return new string[0];

            return (from uir in Db.UserInRoles
                    join r in Db.RolesCms on uir.RolId equals r.Id
                    where uir.UserId == user.Id
                    select r.RoleName).ToArray();

        }

        public static string RolesEditPage(int pageId)
        {
            var role1 = _ps.GetPageById(pageId);
            var role = Db.Pages.FirstOrDefault(c => c.Id == pageId);
            return role != null ? role.EditRoles : null;
        }

        public static string RolesEditModule(int moduleId)
        {
            var role = Db.PageModules.FirstOrDefault(c => c.Id == moduleId);
            return role != null ? role.EditRoles : null;
        }

        public static string RolesDeleteModule(int moduleId)
        {
            var role = Db.PageModules.FirstOrDefault(c => c.Id == moduleId);
            return role != null ? role.DeleteRoles : null;
        }
    }
}
