using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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


        public SelectList GetRoles(string selected = null)
        {
            var id = Convert.ToInt16(selected ?? "0");
            var repo = _context.Roles.Where(o => !o.IsDeleted);
            return new SelectList(repo.Select(r => new SelectListItem { Text = r.RoleName, Value = r.CodRole, Selected = (r.Id == id) }).ToList(), "Value", "Text",repo);
        }

        public SelectList GetUser(string selected = null)
        {
            var id = Convert.ToInt16(selected ?? "0");
            var repo = _context.Users.Where(o => !o.IsDeleted);
            return new SelectList(repo.Select(r => new SelectListItem { Text = r.UserName, Value = r.CodUser, Selected = (r.Id == id) }).ToList(), "Value", "Text");
        }
    }
}
