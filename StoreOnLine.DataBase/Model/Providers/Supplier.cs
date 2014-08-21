using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Model.Providers
{
    public class Supplier : DataBaseId
    {
        public String SupplierName { get; set; }
        public String SupplierDescription { get; set; }
        public String SupplierDocument { get; set; }

        public IEnumerable<Contact> SupplierRepreseContacts { get; set; }
        public IEnumerable<Product> SupplierProducts { get; set; }
    }
}
