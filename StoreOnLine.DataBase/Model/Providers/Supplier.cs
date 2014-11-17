using System;
using System.Collections.Generic;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.CmsProduct;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Model.Providers
{
    public class Supplier : DataBaseId
    {
        public String SupplierName { get; set; }
        public String SupplierDescription { get; set; }
        public String SupplierDocument { get; set; }

        public List<Contact> SupplierRepreseContacts { get; set; }
        public List<Product> SupplierProducts { get; set; }
    }
}
