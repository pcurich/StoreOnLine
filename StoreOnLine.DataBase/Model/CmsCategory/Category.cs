namespace StoreOnLine.DataBase.Model.CmsCategory
{
    public class Category : DataBaseId
    {
        public CategoryLang CategoryLang { get; set; }
        public int CategoryLangId { get; set; }

        public int DepthLevel { get; set; }
        public int Nleft { get; set; }
        public int Nright { get; set; }
        public int Position { get; set; }
        public bool IsRootCategory { get; set; }

    }
}
