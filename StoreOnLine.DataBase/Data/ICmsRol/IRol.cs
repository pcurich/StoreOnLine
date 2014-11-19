using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Model.CmsRol;

namespace StoreOnLine.DataBase.Data.ICmsRol
{
    public interface IRol : IRepository<Rol>
    {
        IQueryable<Rol> GetRolByLanguage(int languageId);
    }
}
