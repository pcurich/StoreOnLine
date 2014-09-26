using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Security
{
    public class Document : DataBaseId
    {
        public int DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }

        public string DocumentValue { get; set; }
        public bool IsPrincipal { get; set; }

        public Document()
        {
        }

        public Document(int id, string documentValue, int documentTypeId)
        {
            Id = id;
            DocumentValue = documentValue;
            IsPrincipal = true;
            DocumentTypeId = documentTypeId;
        }
    }
}
