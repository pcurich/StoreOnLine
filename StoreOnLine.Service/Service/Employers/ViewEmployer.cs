using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.CMS;

namespace StoreOnLine.Service.Service.Employers
{
    public class ViewEmployer
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastPassWordGenered { get; set; }
        public string RolName { get; set; }
        public string ShopName { get; set; }

    }
}
