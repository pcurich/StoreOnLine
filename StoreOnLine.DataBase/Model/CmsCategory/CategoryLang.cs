using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StoreOnLine.DataBase.Model.CmsLanguage;
using StoreOnLine.DataBase.Model.CmsRol;

namespace StoreOnLine.DataBase.Model.CmsCategory
{
    public class CategoryLang : DataBaseId
    {
        public Language Language { get; set; }
        public int LanguageId { get; set; }

        [Display(Name = "CategoryName", ResourceType = typeof(StoreOnLine.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(StoreOnLine.Resources.Resources), ErrorMessageResourceName = "CategoryNameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(StoreOnLine.Resources.Resources), ErrorMessageResourceName = "CategoryNameStringLenght")]
        public string Name { get; set; }

        public string Description { get; set; }
        public string LinkRewrite { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }


        public IList<CategoryRol> CategoryRols { get; set; }

        public void  AddRols(IEnumerable<Rol> rols, int categoryId)
        {
            CategoryRols = new List<CategoryRol>();
            foreach (var rol in rols)
            {
                CategoryRols.Add(new CategoryRol { Active = true, RolId = rol.Id, CategoryLangId = categoryId, Rol = rol});
            }
        }

    }
}
