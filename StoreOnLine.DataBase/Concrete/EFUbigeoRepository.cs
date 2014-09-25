using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Security;
using System.Collections.Generic;
using System.Linq;

namespace StoreOnLine.DataBase.Concrete
{
    public class UbigeoRepository : IUbigeoRepository
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

        public SelectList GetDepart(string selected = null)
        {
            var repo = _context.Ubigeos.Where(o => !o.IsDeleted && o.CodDist == "0" && o.CodProv == "0");
            return new SelectList(repo.Select(r => new SelectListItem { Text = r.NameUbiGeo, Value = r.CodDpto, Selected = (r.CodDpto == selected) }).ToList(), "Value", "Text");
        }

        public SelectList GetProvince(string codDpto, string selected = null)
        {
            var repo = _context.Ubigeos.Where(o => !o.IsDeleted && o.CodDpto == codDpto && o.CodDist == "0" && o.CodProv != "0").ToList();
            return new SelectList(repo.Select(r => new SelectListItem { Text = r.NameUbiGeo, Value = r.CodProv, Selected = (r.CodProv == selected) }).ToList(), "Value", "Text");
        }

        public SelectList GetDistrict(string codDpto, string codProv, string selected = null)
        {
            var repo = _context.Ubigeos.Where(o => !o.IsDeleted && o.CodDpto == codDpto && o.CodProv == codProv && o.CodDist != "0" && o.NameUbiGeo!="");
            return new SelectList(repo.Select(r => new SelectListItem { Text = r.NameUbiGeo, Value = r.CodDist, Selected = (r.CodDist == selected) }).ToList(), "Value", "Text");
        }

        public Ubigeo GetOneDistrict(string codDist)
        {
            var repo = _context.Ubigeos.FirstOrDefault(o => !o.IsDeleted && o.CodDist == codDist);
            return repo;
        }
    }
}
