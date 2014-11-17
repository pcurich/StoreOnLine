using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.CmsGroup;

namespace StoreOnLine.DataBase.Model.CmsShop
{
    public class ShopGroup : DataBaseId
    {
        public Shop Shop { get; set; }
        public int ShopId { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public string Name { get; set; }//Default
        public bool SharedCustumer { get; set; }
        public bool SharedOrder { get; set; }
        public bool SharedStock { get; set; }

    }
}
