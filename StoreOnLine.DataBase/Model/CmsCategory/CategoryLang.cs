using System.ComponentModel.DataAnnotations;
using StoreOnLine.DataBase.Model.CmsLanguage;

namespace StoreOnLine.DataBase.Model.CmsCategory
{
    public class CategoryLang : DataBaseId
    {
        public Language Language { get; set; }
        public int LanguageId { get; set; }

        [Display(Name = "CategoryName", ResourceType = typeof(StoreOnLine.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(StoreOnLine.Resources.Resources),ErrorMessageResourceName = "CategoryNameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(StoreOnLine.Resources.Resources),ErrorMessageResourceName = "CategoryNameStringLenght")]
        public string Name { get; set; }

        public string Description { get; set; }
        public string LinkRewrite { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }

    }
}
