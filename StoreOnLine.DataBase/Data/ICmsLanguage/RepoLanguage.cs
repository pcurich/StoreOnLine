using System.Data.Entity;
using System.Linq;
using StoreOnLine.DataBase.Model.CmsLanguage;

namespace StoreOnLine.DataBase.Data.ICmsLanguage
{
    public class RepoLanguage : StoreRepository<Language>, ILanguage
    {
        private Language _currentLanguage;

        public RepoLanguage(DbContext dbContext, string user)
            : base(dbContext, user)
        {
            _currentLanguage = null;
        }

        public IQueryable<Language> GetLanguageByTwoLetter(string twoLetter)
        {
            return GetAll().Where(o => o.IsoCode == twoLetter);
        }

        public Language GetLanguageByCultura(string culture)
        {
            return _currentLanguage ?? (_currentLanguage = GetAll().First(o => o.Cultura == culture));
        }

        public Language GetLanguageByCountry(string country)
        {
            return _currentLanguage ?? (_currentLanguage = GetAll().First(o => o.Country == country));
        }

        public Language GetCurrentLanguage()
        {
            return _currentLanguage;
        }
    }
}
