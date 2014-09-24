using System.Web.Mvc;
using StoreOnLine.DataBase.Model.Security;
using System.Collections.Generic;

namespace StoreOnLine.DataBase.Abstract
{
    public interface IUbigeoRepository
    {
        IEnumerable<Ubigeo> Ubigeos { get; }
        SelectList GetDepart();
        SelectList GetProvince(string codDpto);
        SelectList GetDistrict(string codDpto, string codProv);
        Ubigeo GetOneDistrict(string codDist);
    }
}
