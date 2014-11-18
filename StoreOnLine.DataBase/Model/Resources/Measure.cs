using System;

namespace StoreOnLine.DataBase.Model.Resources
{
    public class Measure : DataBaseId
    {
        public String ParamName { get; set; }
        public String ParamValue { get; set; }
    }
}
