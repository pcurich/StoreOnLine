using System;
using StoreOnLine.DataBase.Model.CmsProduct;

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
