using StoreOnLine.DataBase.Model.CmsLanguage;

namespace StoreOnLine.DataBase.Model.CmsRol
{
    public class Rol:DataBaseId
    {
        public Language Language { get; set; }
        public int LanguageId { get; set; }

        public string Name { get; set; }
    }
}
