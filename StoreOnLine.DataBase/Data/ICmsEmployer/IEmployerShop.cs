using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Model.CmsEmploye;

namespace StoreOnLine.DataBase.Data.ICmsEmployer
{
    public interface IEmployerShop : IRepository<EmployerShop>
    {
        EmployerShop GetCurrentEmployherByShop(int shopId);
    }
}