using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.CMS
{
    public class Language : DataBaseId
    {
        public string TwoLetterIsoLanguageName { get; set; }
        public string LanguageName { get; set; }
        public string LanguageImageName { get; set; }
        public bool Public { get; set; }
    }
    //Esto se usara para el lenguaje en, fr ...
}
