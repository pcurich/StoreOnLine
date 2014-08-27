using StoreOnLine.DataBase.Model.Shopping;

namespace StoreOnLine.Areas.Shopping.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}