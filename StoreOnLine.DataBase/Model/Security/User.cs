using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Security
{
    public class User : DataBaseId
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public User()
        {
        }

        public User(int id, string userCode, string userName, string userPassword)
        {
            Id = id;
            UserCode = userCode;
            UserName = userName;
            UserPassword = userPassword;
        }

    }
}
