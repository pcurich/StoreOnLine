using System;
using System.ComponentModel.DataAnnotations;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.CMS
{
    public class PageModules : DataBaseId
    {
        public int PageId { get; set; }
        public string ModuleTitle { get; set; }
        public bool ShowTitle { get; set; }
        public string ModuleKeyWord { get; set; }
       // [UIHint("ModulePositionDropDown")]
        public string Position { get; set; }
        public bool ShowOnAllPages { get; set; }
        [DataType("Integer")]
       // [UIHint("Integer"), Required]
        public int ModuleOrder { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        //[UIHint("ModuleDefinition")]
        public int ModuleDefId { get; set; }
        public string ViewRoles { get; set; }
        public string EditRoles { get; set; }
        public string DeleteRoles { get; set; }
        public string Icon { get; set; }
        public bool Publish { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public PageModules()
        {
            ShowOnAllPages = false;
            ShowTitle = true;
            Publish = true;
        }
    }

    //Una paguina tiene referencia a un ModuleId
    //Esta clase permite linkear a un agente para que interactue con una paguina y modulo
    //Ejemplo cuando un usuario haga click sobre HomePage
    //entonces se sabe que modulos estan ligados con HomePage x ejemplo (Html Module Calendar Module Product Module)
    //y cuando el modulo sea cargada en la paguina esta sabra que informacion mostrara al usuario

}
