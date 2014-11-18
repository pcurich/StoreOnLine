using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.CmsLanguage
{
    public class Language:DataBaseId
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string IsoCode { get; set; }
        public string Cultura { get; set; }
    }
}
