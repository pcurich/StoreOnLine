using System.Web;

namespace StoreOnLine.Service.Security
{
    public class SessionPersister
    {
        private const string UsernameSessionVar = "username";

        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session[UsernameSessionVar] != null)
                {
                    return HttpContext.Current.Session[UsernameSessionVar] as string;
                }
                return HttpContext.Current == null ? string.Empty : HttpContext.Current.User.Identity.Name;
            }
            set
            {
                HttpContext.Current.Session[UsernameSessionVar] = value;
            }
        }
    }
}
