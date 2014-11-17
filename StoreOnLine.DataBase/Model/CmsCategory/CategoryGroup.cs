using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.CmsGroup;

namespace StoreOnLine.DataBase.Model.CmsCategory
{
    public class CategoryGroup : DataBaseId
    {
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public decimal Reduction { get; set; }
    }
}
