using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.DataBase.Concrete
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly StoreOnLineContext _context = new StoreOnLineContext();

        public IEnumerable<Role> Roles
        {
            get
            {
                return _context.Roles
                    .Where(o => !o.IsDeleted);
            }
        }

        public IEnumerable<User> Users
        {
            get
            {
                return _context.Users
                    .Where(o => !o.IsDeleted);
            }
        }
    }
}
