using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Data
{
    public class LocalizeResources:DataBaseId
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
}
