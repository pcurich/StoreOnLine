using System;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Security
{
    public class Role : DataBaseId
    {
        public string RoleCode { get; set; }
        public string RoleName { get; set; }

        public Role()
        {
        }

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
