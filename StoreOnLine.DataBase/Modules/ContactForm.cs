using StoreOnLine.DataBase.CMS;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using System;
using StoreOnLine.DataBase.Model;

namespace StoreOnLine.DataBase.Modules
{
    public class ContactForm:DataBaseId
    {
        public int ParentId { get; set; }
        //public string SiteId { get; set; }
        public ModuleDefinition ModuleDefinition { get; set; }
        public int ModuleDefinitionId { get; set; }

        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Action { get; set; }
        public DateTime SendDate { get; set; }
    }
}