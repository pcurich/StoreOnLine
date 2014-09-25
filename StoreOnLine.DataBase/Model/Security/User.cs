using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Security
{
    public class User : DataBaseId
    {
        public string CodUser{ get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
