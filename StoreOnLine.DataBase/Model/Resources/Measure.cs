using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Resources
{
    public class Measure : DataBaseId
    {
        public String ParamName { get; set; }
        public String ParamValue { get; set; }
    }
}
