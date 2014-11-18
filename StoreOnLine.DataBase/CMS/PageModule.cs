using System;
using System.ComponentModel.DataAnnotations;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model;

namespace StoreOnLine.DataBase.CMS
{
    /// <summary>
    /// Una paguina tiene referencia a un ModuleDefinition 
    /// Esta clase permite linkear a un agente para que interactue con una paguina y modulo
    /// Ejemplo cuando un usuario haga click sobre HomePage
    /// entonces se sabe que modulos estan ligados con HomePage x ejemplo (Html Module Calendar Module Product Module)
    /// y cuando el modulo sea cargada en la paguina esta sabra que informacion mostrara al usuario
    /// </summary>
    public class PageModule : DataBaseId
    {
        public Page Page { get; set; }
        public int PageId { get; set; }

        public ModuleDefinition ModuleDefinition { get; set; }
        public int ModuleDefinitionId { get; set; }//[UIHint("ModuleDefinition")]

        public string ModuleTitle { get; set; }
        public string ModuleKeyWord { get; set; }
        public string Position { get; set; } // [UIHint("ModulePositionDropDown")]
        public int ModuleOrder { get; set; }

        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string ViewRoles { get; set; }
        public string EditRoles { get; set; }
        public string DeleteRoles { get; set; }
        public string Icon { get; set; }

        public bool ShowOnAllPages { get; set; }
        public bool ShowTitle { get; set; }
        public bool Publish { get; set; }

        public string CreatedByUser { get; set; }
        public DateTime? CreatedDate { get; set; }

        public PageModule()
        {
            ShowOnAllPages = false;
            ShowTitle = true;
            Publish = true;
        }
    }



}
