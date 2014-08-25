using System.Web.Mvc;
using StoreOnLine.DataBase.Model.Shopping;

namespace StoreOnLine.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string SessionKey = "Cart";


        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // get the Cart from the session
            Cart cart = null;
            if (controllerContext.HttpContext.Session != null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[SessionKey];
            }
            // create the Cart if there wasn't one in the session data
            if (cart != null) return cart;
            cart = new Cart();
            if (controllerContext.HttpContext.Session != null)
            {
                controllerContext.HttpContext.Session[SessionKey] = cart;
            }
            // return the cart
            return cart;
        }
    }
}