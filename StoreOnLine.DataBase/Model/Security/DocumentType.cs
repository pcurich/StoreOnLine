using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreOnLine.DataBase.Entities;

namespace StoreOnLine.DataBase.Model.Security
{
    public class Document:DataBaseId
    {
        public string DocumentTypeName { get; set; }
        public string DocumentValue { get; set; }
        public bool IsPrincipal { get; set; }
    }

    public enum DocumentTypeList
    {
        Dni,
        Passport,
        Ruc
    }
}
