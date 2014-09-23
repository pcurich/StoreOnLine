using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Security
{
    public class ContactNumber : DataBaseId
    {
        public string NumberPhone { get; set; }
        public string CellPhone { get; set; }
        public bool IsPrincipal { get; set; }
    }
}
