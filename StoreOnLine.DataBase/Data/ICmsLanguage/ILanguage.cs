using System.Linq;
using StoreOnLine.DataBase.Model.CmsLanguage;

namespace StoreOnLine.DataBase.Data.ICmsLanguage
{
    public interface ILanguage : IRepository<Language>
    {
        IQueryable<Language> GetLanguageByTwoLetter(string twoLetter);
        Language GetLanguageByCultura(string culture);
        Language GetLanguageByCountry(string culture);
        Language GetCurrentLanguage();
    }
}
