using System.ComponentModel.DataAnnotations;

namespace StoreOnLine.DataBase.Model.Data
{
    public class Pages : DataBaseId
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
        
        public Pages()
        {
            PageOrder = 1;
            EnableBreakCumb = true;
            IncludeInMenu = true;
        }
    }
}
