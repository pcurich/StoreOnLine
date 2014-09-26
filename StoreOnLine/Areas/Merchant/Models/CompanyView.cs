using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreOnLine.DataBase.Model.Companies;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.Areas.Merchant.Models
{
    public class CompanyView
    {
        public string CompanyName { get; set; }
        public string CompanyActivity { get; set; }
        public string CompanyCif { get; set; }
        public string CompanySecurityNumber { get; set; }

        public int CompanyAddressId { get; set; }
        public string CompanyAddressLine1 { get; set; }
        public string CompanyAddressLine2 { get; set; }
        public string CompanyAddressReference { get; set; }

        public int CompanyAddressUbigeoId { get; set; }
        public string CompanyAddressUbigeoCodDpto { get; set; }
        public string CompanyAddressUbigeoCodProv { get; set; }
        public string CompanyAddressUbigeoCodDist { get; set; }
        public string CompanyAddressUbigeoNameUbiGeo { get; set; }


        public int CompanyContactNumberId { get; set; }
        public string CompanyContactNumberNumberPhone { get; set; }
        public string CompanyContactNumberCellPhone { get; set; }
        public string CompanyContactNumberEmail { get; set; }
        public bool CompanyContactNumberIsPrincipal { get; set; }

        public int CompanyPersonId { get; set; }
        public string CompanyPersonFirstName { get; set; }
        public string CompanyPersonLastName { get; set; }

        public Company ToBd(CompanyView view)
        {
            var address = new Address(CompanyAddressId, CompanyAddressLine1, CompanyAddressLine2, CompanyAddressReference, CompanyAddressUbigeoId);
            var number = new ContactNumber(CompanyContactNumberId, CompanyContactNumberNumberPhone,CompanyContactNumberCellPhone, CompanyContactNumberEmail);
            return new Company
            {
                CompanyName = view.CompanyName,
                CompanyActivity = view.CompanyActivity,
                CompanyCif = view.CompanyCif,
                CompanySecurityNumber = view.CompanySecurityNumber,
                CompanyAddressId = view.CompanyAddressId,
                Address = address,
                CompanyContactNumberId = view.CompanyContactNumberId,
                ContactNumber = number,
                CompanyPersonId = view.CompanyPersonId
            };
        }

        public CompanyView ToView(Company db)
        {
            return new CompanyView();
        }
    }
}