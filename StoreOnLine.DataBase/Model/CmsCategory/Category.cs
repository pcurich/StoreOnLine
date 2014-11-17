using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.CmsShop;

namespace StoreOnLine.DataBase.Model.CmsCategory
{
    public class Category : DataBaseId
    {
        public int ParentId { get; set; }

        public int DepthLevel { get; set; }
        public int Nleft { get; set; }
        public int Nright { get; set; }
        public int Position { get; set; }
        public bool IsRootCategory { get; set; }

    }
}
