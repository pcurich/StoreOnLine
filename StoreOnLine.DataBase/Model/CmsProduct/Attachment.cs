
using StoreOnLine.DataBase.Model.CmsLanguage;

namespace StoreOnLine.DataBase.Model.CmsProduct
{
    public class Attachment : DataBaseId
    {
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string Mine { get; set; }
        public long FileSize { get; set; }

        public Language Language { get; set; }
        public int LanguageId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
