using StoreOnLine.DataBase.Model.CmsLanguage;

namespace StoreOnLine.DataBase.Model.CmsProduct
{
    public class ProductLang:DataBaseId
    {
        public Language Language { get; set; }
        public int LanguageId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string DescriptionShort { get; set; }
        public string LinkRewrite { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }

        public string AvailableNow { get; set; }
        public string AvailableLater { get; set; }

    }
}
