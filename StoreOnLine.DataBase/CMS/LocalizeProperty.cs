using System.ComponentModel.DataAnnotations;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.CMS
{
    public class LocalizeProperty : DataBaseId
    {
        public Language Language { get; set; }
        public int LanguageId { get; set; }

        [Key]
        public string PropertyType { get; set; }

        [Key]
        public string PropertyKey { get; set; }

        public string PropertyValue { get; set; }
        public string SeoValue { get; set; }
    }
}

//Cada Paguina tiene un nombre y modulo y se va a utilizar esta clase para cambiar el lenguaje }
