using StoreOnLine.DataBase.Model.CmsEmploye;
using System.Data.Entity;
using System.Linq;

namespace StoreOnLine.DataBase.Data.ICmsEmployer
{
    public class RepoEmployer : StoreRepository<Employer>, IEmployer
    {
        private Employer _currentEmployer;

        public RepoEmployer(DbContext dbContext, string user)
            : base(dbContext,user)
        {
        }

        public Employer CurrentEmployer()
        {
            return _currentEmployer;
        }

        public Employer GetEmployersByName(string userName)
        {
            _currentEmployer = GetAll().First(o => o.UserName == userName);
            return _currentEmployer;
        }

         
    }
}