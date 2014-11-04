using StoreOnLine.DataBase.CMS;
using System;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Modules
{
    public class HtmlContent : DataBaseId
    {
        public ModuleDefinition ModuleDefinition { get; set; }
        public int ModuleDefinitionId { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
        public string CultureId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateByUser { get; set; }
    }
}