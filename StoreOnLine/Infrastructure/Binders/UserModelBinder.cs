using System.Web.Mvc;
using StoreOnLine.Service.Service.Employers;

namespace StoreOnLine.Infrastructure.Binders
{
    public class UserModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // get the Cart from the session
            ViewEmployer user = null;
            if (controllerContext.HttpContext.Session != null)
            {
                user = (ViewEmployer)controllerContext.HttpContext.Session[Service.Constants.Enums.EmployerOnLine];
            }
            // create the Cart if there wasn't one in the session data
            if (user != null) return user;
            user = new ViewEmployer();
            if (controllerContext.HttpContext.Session != null)
            {
                controllerContext.HttpContext.Session[Service.Constants.Enums.EmployerOnLine] = user;
            }
            // return the cart
            return user;
        }
    }
}