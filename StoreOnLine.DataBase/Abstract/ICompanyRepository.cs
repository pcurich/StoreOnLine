using StoreOnLine.DataBase.Model.Companies;
using System.Collections.Generic;

namespace StoreOnLine.DataBase.Abstract
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> Companies { get; }
        int SaveCompany(Company company);
        int SaveSchedule(Schedule schedule);
        Company DeleteCompany(int company, bool physical = false);
    }
}
