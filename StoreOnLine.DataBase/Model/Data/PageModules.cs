using System;
using System.ComponentModel.DataAnnotations;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Data
{
    public  class PageModules:DataBaseId
    {
        public int PageId { get; set; }
        public string ModuleTitle { get; set; }
        public bool ShowTitle { get; set; }
        public string ModuleKeyWord { get; set; }
      //  [UIHint("ModulePositionDropDown")]
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
}
