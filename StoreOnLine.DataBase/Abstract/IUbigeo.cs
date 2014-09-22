using System.Web.Mvc;
using StoreOnLine.DataBase.Model.Security;
using System.Collections.Generic;

namespace StoreOnLine.DataBase.Abstract
{
    public interface IUbigeo
    {
        IEnumerable<Ubigeo> Ubigeos { get; }
        List<SelectListItem> GetDepart();
        List<SelectListItem> GetProvince(string codDpto);
        List<SelectListItem> GetDistrict(string codDpto, string codProv);
        Ubigeo GetOneDistrict(string codDist);
    }
}
