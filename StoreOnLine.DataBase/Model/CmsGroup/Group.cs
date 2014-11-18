using System;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.CmsGroup
{
    public class Group : DataBaseId
    {
        public decimal Reduction { get; set; }
        public int PriceDisplayMethod { get; set; }
        public bool ShowPrices { get; set; }
    }
}
