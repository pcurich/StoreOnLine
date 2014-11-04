using System;
using System.Collections.Generic;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.CMS
{
    /// <summary>
    /// Informacion de un usuario
    /// </summary>
    public class User : DataBaseId
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public bool IsApprove { get; set; }
        public DateTime? LastLogInDate { get; set; }
        public DateTime? LastPasswordChangedDate { get; set; }
        public bool IsOnLine { get; set; }
        public bool IsLockOut { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public int FailedPasswordAnswerAttemptCount { get; set; }
        public bool IsDelete { get; set; }

        public List<UserInRoles> UserInRoleses { get; set; }
    }
}
