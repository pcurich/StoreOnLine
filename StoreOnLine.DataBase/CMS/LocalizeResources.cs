using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.CMS
{
    public class LocalizeResources : DataBaseId
    {
        [Key]
        [Column(Order = 1)]
        public string CultureId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string ResourceType { get; set; }
        [Key]
        [Column(Order = 3)]
        public string ResourceKey { get; set; }
        public string ResourceValue { get; set; }
    }

    //Para implementar la localizacion. De las muchas formas de hacer esto Globalization Resource file es la mas simple
    //pero lo voy a obtener de base de datos
}
