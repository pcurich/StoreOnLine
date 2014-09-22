using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Security
{
    public class User : DataBaseId
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
