using System;
using System.Linq;
using System.Web;
using StoreOnLine.Service.Business;

namespace StoreOnLine.Service.Security
{
    public class Role
    {
        public static bool IsDirectCall()
        {
            return !(HttpContext.Current.Request.UrlReferrer != null &&
                HttpContext.Current.Request.UrlReferrer.ToString().Contains(HttpContext.Current.Request.Url.Host));
        }

        public static bool IsAdministrators()
        {
            return HttpContext.Current.User.IsInRole(Enums.Administrators);
        }

        public static bool IsAdministratorOrContentAdministrator()
        {
            return HttpContext.Current.User.IsInRole(Enums.Administrators) ||
                   HttpContext.Current.User.IsInRole(Enums.Contentadministrators);
        }

        public static bool IsPublic(bool Public, DateTime? startDate, DateTime? endDate)
        {
            //Muestra si es administrador
            if (IsAdministratorOrContentAdministrator())
                return true;

            if (!Public)
                return false;

            //llegar aquí significa pública
            if (string.IsNullOrEmpty(startDate.ToString()) && string.IsNullOrEmpty(endDate.ToString()))
                return true;

            //llegar aquí significa que una fecha puede ser nula
            if (string.IsNullOrEmpty(startDate.ToString()))//significa que enddate no es nula
                return endDate > DateTime.Now;

            //significa que enddate es nulo
            return startDate < DateTime.Now;
        }

        //Permite ver los roles o no 
        public static bool IsViewRole(string sRoles)
        {
            if (IsAdministratorOrContentAdministrator())
                return true;

            if (string.IsNullOrEmpty(sRoles) || sRoles.Contains(Enums.Allusers))
                return true;

            var arrRoles = sRoles.Split(';');

            //permite comprobar si un rol de un usuario existe en viewroles
            return arrRoles.Where(t => t != "").Any(t => HttpContext.Current.User.IsInRole(t));
        }

        //Permite imprimir los roles o no 
        public static bool IsPrintRole(string sRoles)
        {
            if (IsAdministratorOrContentAdministrator())
                return true;

            var arrRoles = sRoles.Split(';');

            //permite comprobar si el rol de un usuario existe en los printroles
            return arrRoles.Where(t => t != "").Any(t => HttpContext.Current.User.IsInRole(t.ToLower()));
        }

        //Permite editar los roles o no 
        public static bool IsEditRole(string sRoles)
        {
            if (IsAdministratorOrContentAdministrator())
                return true;

            if (string.IsNullOrEmpty(sRoles))
                return false;
            var arrRoles = sRoles.Split(';');

            //permite comprobar si el rol de un usuario existe en los editroles
            return arrRoles.Where(t => t != "").Any(t => HttpContext.Current.User.IsInRole(t));
        }

        //Permite borrar los roles o no 
        public static bool IsDeleteRole(string sRoles)
        {
            if (IsAdministratorOrContentAdministrator())
                return true;

            if (string.IsNullOrEmpty(sRoles))
                return false;

            var arrRoles = sRoles.Split(';');

            //permite comprobar si el rol de un usuario existe en los editroles
            return arrRoles.Where(t => t != "").Any(t => HttpContext.Current.User.IsInRole(t));
        }

        //Permite validar si un usuario tiene permiso o no de editar un modulo
        public static bool IsEditUser(string userName)
        {
            return !string.IsNullOrEmpty(userName) &&
                String.Equals(HttpContext.Current.User.Identity.Name, userName, StringComparison.CurrentCultureIgnoreCase);
        }

        //Verifica si el rol esta en el arreglo de roles
        public static bool IsContentRole(string sRoles, string sRole)
        {
            if (string.IsNullOrEmpty(sRoles))
                return false;

            var arrRoles = sRoles.Split(';');

            //Si el rol de un usuario existe en editroles
            return arrRoles.Where(t => t != "").Any(t => t.ToLower() == sRole.ToLower());
        }

        public static void NotAdminOrContentAdminReturnToLoginPage()
        {
            if (!IsAdministratorOrContentAdministrator())
            {
                System.Web.Security.FormsAuthentication.RedirectToLoginPage();
            }
        }

        public static void NotAdminReturnToLoginPage()
        {
            if (!HttpContext.Current.User.IsInRole(Enums.Administrators))
            {
                System.Web.Security.FormsAuthentication.RedirectToLoginPage();
            }
        }

        public static void NotPageEditUserReturnToLoginPage(string userName)
        {
            if (!IsEditUser(userName))
            {
                System.Web.Security.FormsAuthentication.RedirectToLoginPage();
            }
        }

        public static void ReturnToLoginPage()
        {
            System.Web.Security.FormsAuthentication.RedirectToLoginPage();
        }

        public static void IfUserNotLoginReturnToLoginPage()
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                System.Web.Security.FormsAuthentication.RedirectToLoginPage();
            }
        }
    }
}
