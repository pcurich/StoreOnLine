using StoreOnLine.DataBase.Entities;
using System;

namespace StoreOnLine.DataBase.Model.Resources
{
    public class Imagen : DataBaseId
    {
        public Byte[] Photo { get; set; }
    }
}
