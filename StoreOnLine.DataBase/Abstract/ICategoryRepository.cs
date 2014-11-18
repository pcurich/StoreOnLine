using System.Collections.Generic;
using StoreOnLine.DataBase.Model.CmsCategory;

namespace StoreOnLine.DataBase.Abstract
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        int SaveCategory(Category category);
        Category DeleteCategory(int categoryId, bool physical=false);
    }
}
