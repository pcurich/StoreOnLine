using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Security
{
    public class Document : DataBaseId
    {
        public string DocumentDni { get; set; }
        public string DocumentRuc { get; set; }

        public bool IsPrincipal { get; set; }

        public Document()
        {
        }

        public Document(int id, string documentDni, string documentRuc)
        {
            Id = id;
            DocumentDni = documentDni;
            IsPrincipal = true;
            DocumentRuc = documentRuc;
        }
    }
}


//public enum DocumentType
//{
//    Dni,
//    Passaporte,
//    Ruc
//}