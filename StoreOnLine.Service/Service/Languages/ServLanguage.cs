using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Model.CmsLanguage;

namespace StoreOnLine.Service.Service.Languages
{
    public class ServLanguage : ServBase
    {
        private readonly int _currentId;
        readonly IDictionary<int, Language> _languages = new Dictionary<int, Language>();

        public ServLanguage()
        {
            var language = (from c in Db.Languages
                            where c.Cultura == Thread.CurrentThread.CurrentCulture.Name
                            select c).ToList().Take(1).FirstOrDefault();
            
            if (language != null)
            {
                _currentId = language.Id;
            }
            _languages.Add(_currentId, language);
        }
    }
}
