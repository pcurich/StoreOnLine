using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.CmsLanguage;

namespace StoreOnLine.DataBase.Model.CmsGender
{
    public class GenderLang : DataBaseId
    {
        public Gender Gender { get; set; }
        public int GenderId { get; set; }

        public Language Language { get; set; }
        public int LanguageId { get; set; }
        
        public string Name { get; set; }
    }
}
