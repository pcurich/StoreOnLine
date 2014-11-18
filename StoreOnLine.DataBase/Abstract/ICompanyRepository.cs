using System.Collections.Generic;
using StoreOnLine.DataBase.Model.Companies;

namespace StoreOnLine.DataBase.Abstract
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> Companies { get; }
        IEnumerable<Schedule> Schedules { get; }
        int SaveCompany(Company company);
        Company DeleteCompany(int company, bool physical = false);
    }
}
