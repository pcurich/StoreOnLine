using System;
using StoreOnLine.DataBase.Configuration;
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
        SuperAdmin,
        Supervisor,
        Empleado,
        Representante,
        Admin,
        UserAdmin,
        UserAudit,
        UserSales,
        Customer,
        Guest,
        User
    }
}
