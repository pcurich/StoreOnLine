using System;
using System.Linq;
using System.Web.Security;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.Infrastructure.Security
{
    public class StoreOnLIneRoleProvider : RoleProvider
    {
        private readonly StoreOnLineContext _db = new StoreOnLineContext();


        public override bool IsUserInRole(string username, string roleName)
        {
            var userInRol = (from ur in _db.Employers
                             join r in _db.Rols on ur.RolId equals r.Id
                             where (r.Name == roleName && (ur.Email == username || ur.UserName == username))
                             select ur).FirstOrDefault();

            return userInRol != null;
        }

        public override string[] GetRolesForUser(string username)
        {
            return (from e in _db.Employers
                    join r in _db.Rols on e.RolId equals r.Id
                    where (e.Email == username || e.UserName == username)
                    select r.Name).ToArray();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}