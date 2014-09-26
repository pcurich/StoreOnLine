using StoreOnLine.DataBase.Model.Security;
using System.Web.Mvc;

namespace StoreOnLine.Infrastructure.Binders
{
    public class UserModelBinder : IModelBinder
    {
        private const string SessionKey = "UserOnLine";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // get the Cart from the session
            User user = null;
            if (controllerContext.HttpContext.Session != null)
            {
                user = (User)controllerContext.HttpContext.Session[SessionKey];
            }
            // create the Cart if there wasn't one in the session data
            if (user != null) return user;
            user = new User();
            if (controllerContext.HttpContext.Session != null)
            {
                controllerContext.HttpContext.Session[SessionKey] = user;
            }
            // return the cart
            return user;
        }
    }
}