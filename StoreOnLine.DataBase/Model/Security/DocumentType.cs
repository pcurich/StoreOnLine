using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Security
{
    public class DocumentType : DataBaseId
    {
        public string DocumentTypeName { get; set; }

        public DocumentType()
        {
        }

        public DocumentType(int id)
        {
            Id = id;
        }
    }
}
