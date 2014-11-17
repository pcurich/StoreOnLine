using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.CmsShop
{
    public class Shop : DataBaseId
    {
        public string Name { get; set; }//StoreOnLine
        public int Theme { get; set; }
    }
}
