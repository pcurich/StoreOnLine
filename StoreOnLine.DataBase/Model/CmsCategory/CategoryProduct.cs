using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.CmsProduct;

namespace StoreOnLine.DataBase.Model.CmsCategory
{
    public class CategoryProduct:DataBaseId 
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Position { get; set; }
    }
}
