using StoreOnLine.DataBase.Abstract;
using System;
using System.Linq;
using System.Web.Security;

namespace StoreOnLine.Infrastructure.Security
{
    public class StoreOnLIneRoleProvider : RoleProvider
    {
        private readonly IPersonRepository _repositoryPerson;

        public StoreOnLIneRoleProvider(IPersonRepository repositoryPerson)
        {
            _repositoryPerson = repositoryPerson;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var userInRol = _repositoryPerson.Persons.FirstOrDefault(o => o.User.UserName == username && o.Role.RoleName == roleName);
            if(userInRol!=null) return true;
            userInRol = _repositoryPerson.Persons.FirstOrDefault(o => o.User.UserCode == username && o.Role.RoleName == roleName);
            if (userInRol != null) return true;
            return false;
        }

        public override string[] GetRolesForUser(string username)
        {
            return (from o in _repositoryPerson.Persons where o.Role.RoleName == username select o.Role.RoleName).ToArray();
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