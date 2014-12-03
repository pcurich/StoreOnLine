
namespace StoreOnLine.DataBase.Model.CmsProduct
{
    public class Attachment : DataBaseId
    {
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string Mine { get; set; }
        public long FileSize { get; set; }
    }
}
