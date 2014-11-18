using System;

namespace StoreOnLine.DataBase.Model.Resources
{
    public class Imagen : DataBaseId
    {
        public int ObjectId { get; set; }
        public String ObjectName { get; set; }
        public int Secuence { get; set; }
        public Byte[] ImageData { get; set; }
        public String ImageMimeType { get; set; }
        public Boolean IsPrincipal { get; set; }

        public Imagen()
        {
            IsPrincipal = true;
            Active = true;
        }
    }
}
