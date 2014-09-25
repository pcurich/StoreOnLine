using System;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Security
{
    public class Role : DataBaseId
    {
        public string CodRole { get; set; }
        public string RoleName { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }

    public enum RoleList
    {
        Admin,
        UserAdmin,
        UserAudit,
        UserSales,
        Customer,
        Guest
    }
}
