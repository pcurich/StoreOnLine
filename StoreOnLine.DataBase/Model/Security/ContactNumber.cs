using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Security
{
    public class ContactNumber : DataBaseId
    {
        public string NumberPhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public bool IsPrincipal { get; set; }

        public ContactNumber()
        {
        }

        public ContactNumber(int id, string numberPhone, string cellPhone,string email)
        {
            Id = id;
            NumberPhone = numberPhone;
            CellPhone = cellPhone;
            Email = email;
            IsPrincipal = true;
        }
    }
}
