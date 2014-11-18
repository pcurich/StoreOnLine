using System.Linq;
using StoreOnLine.DataBase.Model.CmsEmploye;

namespace StoreOnLine.DataBase.Data.ICmsEmployer
{
    public interface IEmployer : IRepository<Employer>
    {
        Employer CurrentEmployer();
        Employer GetEmployersByName(string userName);
    }
}