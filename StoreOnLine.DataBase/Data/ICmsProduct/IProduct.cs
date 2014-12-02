using StoreOnLine.DataBase.Model.CmsProduct;

namespace StoreOnLine.DataBase.Data.ICmsProduct
{
    public interface IProduct: IRepository<Product>
    {
        Product GetProductByProductLang(int productLangId);
    }
}
