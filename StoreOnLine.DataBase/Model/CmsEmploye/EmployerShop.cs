using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.CmsShop;

namespace StoreOnLine.DataBase.Model.CmsEmploye
{
    public class EmployerShop:DataBaseId
    {
        public Employer Employer { get; set; }
        public int EmployeId { get; set; }

        public Shop Shop { get; set; }
        public int ShopId { get; set; }
    }
}
