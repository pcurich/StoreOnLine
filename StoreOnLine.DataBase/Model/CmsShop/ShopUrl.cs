using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.CmsShop
{
    public class ShopUrl: DataBaseId
    {
        public Shop Shop { get; set; }
        public int ShopId { get; set; }

        public string Domain { get; set; }
        public string DomainSsl { get; set; }
        public string PhysicalUrl { get; set; }
        public string VirtualUrl { get; set; }
        public bool Main { get; set; }


    }
}
