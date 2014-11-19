using StoreOnLine.DataBase.Model.CmsRol;

namespace StoreOnLine.DataBase.Model.CmsCategory
{
    public class CategoryRol : DataBaseId
    {
        public CategoryLang CategoryLang { get; set; }
        public int CategoryLangId { get; set; }
        
        public int RolId { get; set; }
        public Rol Rol { get; set; }

        public bool HasPermition { get; set; }
    }
}
