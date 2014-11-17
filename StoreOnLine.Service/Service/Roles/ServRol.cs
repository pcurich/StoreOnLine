using System.Collections.Generic;
using System.Linq;
using System.Threading;
using StoreOnLine.DataBase.Model.CmsRol;

namespace StoreOnLine.Service.Service.Roles
{
    public class ServRol : ServBase
    {
        private int _currentId;
        private readonly IDictionary<int, Rol> _rols = new Dictionary<int, Rol>();

        public ServRol()
        {
            var rols = (from r in Db.Rols
                        join l in Db.Languages on r.LanguageId equals l.Id
                        where l.Cultura == Thread.CurrentThread.CurrentCulture.Name
                        select r).ToList();

            foreach (var rol in rols)
            {
                _rols.Add(rol.Id, rol);
            }
        }

        public void SetCurrentRol(int rolId)
        {
            _currentId = rolId;
        }

        public Rol GetCurrentRol()
        {
            return _rols[_currentId];
        }

    }
}
