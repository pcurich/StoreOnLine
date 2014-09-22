using System;
using System.Web.Mvc;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Security;
using System.Collections.Generic;
using System.Linq;

namespace StoreOnLine.DataBase.Concrete
{
    public class UbigeoRepository : IUbigeo
    {
        private readonly StoreOnLineContext _context = new StoreOnLineContext();

        public IEnumerable<Ubigeo> Ubigeos
        {
            get
            {
                return _context.Ubigeos
                    .Where(o => !o.IsDeleted);
            }
        }

        public List<SelectListItem> GetDepart()
        {
            var repo = _context.Ubigeos.Where(o => !o.IsDeleted).Distinct();
            return repo.Select(r => new SelectListItem { Text = r.NameUbiGeo, Value = Convert.ToString(r.CodDpto) }).ToList();
        }

        public List<SelectListItem> GetProvince(string codDpto)
        {
            var repo = _context.Ubigeos.Where(o => !o.IsDeleted && o.CodDpto == codDpto).Distinct();
            return repo.Select(r => new SelectListItem { Text = r.NameUbiGeo, Value = Convert.ToString(r.CodProv) }).ToList();
        }

        public List<SelectListItem> GetDistrict(string codDpto, string codProv)
        {
            var repo = _context.Ubigeos.Where(o => !o.IsDeleted && o.CodDpto == codDpto && o.CodProv == codProv).Distinct();
            return repo.Select(r => new SelectListItem { Text = r.NameUbiGeo, Value = Convert.ToString(r.CodDist) }).ToList();
        }

        public Ubigeo GetOneDistrict(string codDist)
        {
            var repo = _context.Ubigeos.FirstOrDefault(o => !o.IsDeleted && o.CodDist == codDist);
            return repo;
        }
    }
}
