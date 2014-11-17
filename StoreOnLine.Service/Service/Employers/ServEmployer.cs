using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using StoreOnLine.DataBase.Model.CmsEmploye;

namespace StoreOnLine.Service.Service.Employers
{
    public class ServEmployer : ServBase
    {
        private static ServEmployer _instance;
        private static readonly Object Mutex = new Object();
        

        private readonly IDictionary<string, Employer> _employers = new Dictionary<string, Employer>();
        private readonly IDictionary<int, EmployerShop> _employerShops = new Dictionary<int, EmployerShop>();

        private readonly int _currentShopId;
        public string CurrentEmployer { get; set; }

        public static ServEmployer Instance(string employerName)
        {
            if (_instance == null)
            {
                lock (Mutex) 
                {
                    if (_instance == null)
                    {
                        _instance = new ServEmployer(employerName);
                    }
                }
            }
            
            if (!_instance.CurrentEmployer.Equals(employerName))
            {
                _instance.CurrentEmployer = employerName;
            }
            return _instance;
        }

        public ServEmployer(string employerName)
        {
            var isInCache = IsRegisted(employerName);
            if (isInCache)
                return;

            var enployerOnline = (from employer in Db.Employers
                                  join employerShop in Db.EmployerShops on employer.Id equals employerShop.Id
                                  where ((employer.UserName == employerName) || employer.Email == employerName)
                                  select new { employer.Id, employerShop.ShopId }).First();

            CurrentEmployer = employerName;
            _currentShopId = enployerOnline.ShopId;

            //Empleados de una tienda
            var employeShops = (from e in Db.EmployerShops
                                where e.ShopId == _currentShopId
                                select e).ToList();

            foreach (var employerShop in employeShops)
            {
                employerShop.Shop = (from s in Db.Shops
                                     where s.Id == employerShop.ShopId
                                     select s).First();

                employerShop.Employer = (from s in Db.Employers
                                         where s.Id == employerShop.Id
                                         select s).First();

                _employerShops.Add(employerShop.EmployeId, employerShop);
            }

            var employers = (from e in Db.Employers
                             join es in Db.EmployerShops on e.Id equals es.EmployeId
                             where es.ShopId == _currentShopId
                             select e).ToList();

            foreach (var employer in employers)
            {
                employer.Rol = (from r in Db.Rols
                                join e in Db.Employers on r.Id equals e.RolId
                                select r).FirstOrDefault();
                _employers.Add(employer.Email, employer);
                _employers.Add(employer.UserName, employer);
            }
        }

        private bool IsRegisted(string employerName)
        {
            return (_employers != null && _employers.ContainsKey(employerName));
        }

        public Employer GetCurrentEmployer()
        {
            return _employers[CurrentEmployer];
        }

        public ViewEmployer GetCurrentViewEmployer()
        {
            var employer = GetCurrentEmployer();
            var employerShop = _employerShops[employer.Id];
            var viewEmployer = new ViewEmployer
            {
                Email = employer.Email,
                Id = employer.Id,
                UserName = employer.UserName,
                LastName = employer.LastName,
                FirstName = employer.FirstName
            };
            viewEmployer.Email = employer.Email;
            viewEmployer.Password = employer.Password;
            viewEmployer.LastPassWordGenered = employer.LastPassWordGenered;
            viewEmployer.RolName = employer.Rol.Name;
            viewEmployer.ShopName = employerShop.Shop.Name;
            return viewEmployer;
        }
    }
}
