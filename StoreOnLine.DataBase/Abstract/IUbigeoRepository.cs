using System.Web.Mvc;
using StoreOnLine.DataBase.Model.Security;
using System.Collections.Generic;

namespace StoreOnLine.DataBase.Abstract
{
    public interface IUbigeoRepository
    {
        IEnumerable<Ubigeo> Ubigeos { get; }
        SelectList GetDepart(string selected);
        SelectList GetProvince(string codDpto, string selected);
        SelectList GetDistrict(string codDpto, string codProv, string selected);
        Ubigeo GetOneDistrict(string codDist);
    }
}
