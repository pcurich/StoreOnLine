
namespace StoreOnLine.DataBase.Model.CmsProduct
{
    public class ProductAttachment:DataBaseId
    {
        public Attachment Attachment { get; set; }
        public int AttachmentId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
