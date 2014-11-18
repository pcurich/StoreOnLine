using StoreOnLine.DataBase.Model.CmsLanguage;

namespace StoreOnLine.DataBase.Model.CmsGroup
{
    public class GroupLang : DataBaseId
    {
        public Group Group { get; set; }
        public int GroupId { get; set; }

        public Language Language { get; set; }
        public int LanguageId { get; set; }

        public string Name { get; set; }
    }
}
