﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreOnLine.DataBase.Model.Data
{
    public class LocalizeProperties:DataBaseId
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
}
