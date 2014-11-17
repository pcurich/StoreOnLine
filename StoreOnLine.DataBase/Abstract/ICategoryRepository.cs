using System.Collections.Generic;
using StoreOnLine.DataBase.Model.CmsCategory;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Abstract
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        int SaveCategory(Category category);
        Category DeleteCategory(int categoryId, bool physical=false);
    }
}
