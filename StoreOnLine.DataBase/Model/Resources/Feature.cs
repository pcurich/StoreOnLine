using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.CmsProduct;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Model.Resources
{
    public class Feature : DataBaseId
    {
        public String ParamName { get; set; }
        public String ParamValue { get; set; }

        public Measure Measure { get; set; }
        public int MeasureId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
