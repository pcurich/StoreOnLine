using System.Web.Mvc;
using StoreOnLine.DataBase.Model.CmsEmploye;

namespace StoreOnLine.Infrastructure.Binders
{
    public class EmployerModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // get the user from the session
            Employer user = null;
            if (controllerContext.HttpContext.Session != null)
            {
                user = (Employer)controllerContext.HttpContext.Session[Service.Constants.Enums.EmployerOnLine];
            }
            // create the Cart if there wasn't one in the session data
            if (user != null) 
                return user;

            user = new  Employer();
            if (controllerContext.HttpContext.Session != null)
            {
                controllerContext.HttpContext.Session[Service.Constants.Enums.EmployerOnLine] = user;
            }
            // return the user
            return user;
        }
    }
}