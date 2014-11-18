using StoreOnLine.DataBase.Model.CmsShop;

namespace StoreOnLine.DataBase.Model.CmsLanguage
{
    public class LanguageShop : DataBaseId
    {
        public Language Language { get; set; }
        public int LanguageId { get; set; }

        public Shop Shop  { get; set; }
        public int ShopId { get; set; }

    }
}
