using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.CMS
{
    public class LocalizeProperties : DataBaseId
    {
        [Key]
        [Column(Order = 1)]
        public string CultureId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string PropertyType { get; set; }
        [Key]
        [Column(Order = 3)]
        public string PropertyKey { get; set; }
        public string PropertyValue { get; set; }
        public string SeoValue { get; set; }
    }

    //Cada Paguina tiene un nombre y modulo y se va a utilizar esta clase para cambiar el lenguaje 
}
