using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.CmsShop;

namespace StoreOnLine.DataBase.Model.CmsCategory
{
    public class CategoryShop:DataBaseId
    {
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public Shop Shop { get; set; }
        public int ShopId { get; set; }

        public int Position { get; set; }
    }
}
