using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using System.ComponentModel.DataAnnotations;
using StoreOnLine.DataBase.Model;

namespace StoreOnLine.DataBase.CMS
{
    public class Page : DataBaseId
    {
        public int ParentId { get; set; }
        [DataType("Integer")]
        public int PageOrder { get; set; }
        public string PageName { get; set; }
        public string ViewRoles { get; set; }
        public string EditRoles { get; set; }
        public string CreateContentRoles { get; set; }
        public string PageDescription { get; set; }
        public string Keywords { get; set; }
        public string Url { get; set; }
        public bool OpenInNewWindow { get; set; }
        public bool IncludeInMenu { get; set; }
        public bool EnableComments { get; set; }
        public bool EnableBreakCumb { get; set; }
        public bool ShowChildLinks { get; set; }

        public Page()
        {
            PageOrder = 1;
            EnableBreakCumb = true;
            IncludeInMenu = true;
        }
    }

    //
    //Esta clase permite mostrar las paguinas como un template de varios modulos dentro de una misma paguina 
    //Tiene una referencia a ModuleId para llamar por ahi a la informacion que se desea mostrar 
    //Tambien se le asigna permisos a la paguina ya sea para usuarios o grupos de usuarios
    //Home Page          cualquiera puede ver
    //Accounting Page    solo la prodran ver usuarios con nivel de acceso Accounting 
}
