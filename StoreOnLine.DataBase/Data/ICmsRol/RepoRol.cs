using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Model.CmsRol;

namespace StoreOnLine.DataBase.Data.ICmsRol
{
    public class RepoRol : StoreRepository<Rol>, IRol
    {
        public RepoRol(DbContext dbContext, string user) : base(dbContext, user)
        {
        }

        public IQueryable<Rol> GetRolByLanguage(int languageId)
        {
            return GetAll().Where(o => o.LanguageId == languageId);
        }
    }
}
