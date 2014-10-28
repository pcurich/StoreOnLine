using System.Linq;
using System.Web;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.Service.Security
{
    public class CurrentUser
    {
        static StoreOnLineContext db = new StoreOnLineContext();
        //static PageModuleService pms = new PageModuleService();
        //static PageService ps = new PageService();

        public static bool IsInRole(string role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }

        public static int GetUserId()
        {
            var user = db.Users.SingleOrDefault(c => c.UserName == HttpContext.Current.User.Identity.Name);
            if (user != null)
            {
                return user.Id;
            }
            return -1;
        }

        public static string[] GetRoles()
        {
            return !HttpContext.Current.Request.IsAuthenticated ? new string[0] :
                (from u in
                     db.UserInRoles.Where(c => c.UserName == HttpContext.Current.User.Identity.Name)
                 select u.RoleName).ToArray();
        }

        public static string RolesEditPage(int pageId)
        {
            var role = db.Pages.FirstOrDefault(c => c.Id == pageId);
            return role != null ? role.EditRoles : null;
        }

        public static string RolesEditModule(int moduleId)
        {
            var role = db.PageModules.FirstOrDefault(c => c.Id == moduleId);
            return role != null ? role.EditRoles : null;
        }

        public static string RolesDeleteModule(int moduleId)
        {
            var role = db.PageModules.FirstOrDefault(c => c.Id == moduleId);
            return role != null ? role.DeleteRoles : null;
        }
    }
}
