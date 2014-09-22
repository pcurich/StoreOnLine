using System.Security.Cryptography.X509Certificates;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Security
{

    public class Address : DataBaseId
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public Ubigeo Ubigeo { get; set; }
        public bool IsPrincipal { get; set; }
    }

}
