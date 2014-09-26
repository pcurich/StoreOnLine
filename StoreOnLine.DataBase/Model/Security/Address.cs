using System.Security.Cryptography.X509Certificates;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Security
{

    public class Address : DataBaseId
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Reference { get; set; }

        public int UbigeoId { get; set; }
        public Ubigeo Ubigeo { get; set; }

        public bool IsPrincipal { get; set; }

        public Address()
        {
        }

        public Address(int id, string line1, string line2, string reference, int ubigeoId)
        {
            Id = id;
            Line1 = line1;
            Line2 = line2;
            Reference = reference;
            UbigeoId = ubigeoId;
            IsPrincipal = true;
        }
    }

}
