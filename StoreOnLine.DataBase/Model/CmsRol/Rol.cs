using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.CmsLanguage;

namespace StoreOnLine.DataBase.Model.CmsRol
{
    public class Rol:DataBaseId
    {
        public Language Language { get; set; }
        public int LanguageId { get; set; }

        public string Name { get; set; }
    }
}
