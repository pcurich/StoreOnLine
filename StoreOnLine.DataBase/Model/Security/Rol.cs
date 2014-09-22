using System;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Security
{
    public class Role : DataBaseId
    {
        public string RoleName { get; set; }
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
