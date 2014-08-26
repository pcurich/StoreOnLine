using StoreOnLine.DataBase.Model.Shopping;

namespace StoreOnLine.DataBase.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
