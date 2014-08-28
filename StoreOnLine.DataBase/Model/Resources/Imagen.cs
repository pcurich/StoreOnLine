using StoreOnLine.DataBase.Entities;
using System;

namespace StoreOnLine.DataBase.Model.Resources
{
    public class Imagen : DataBaseId
    {
        public Byte[] ImageData { get; set; }
        public String ImageMimeType { get; set; }
        public Boolean IsPrincipal { get; set; }
    }
}
