using StoreOnLine.DataBase.Model.Resources;
using System.Collections.Generic;

namespace StoreOnLine.DataBase.Abstract
{
    public interface IImagenRepository
    {
        IEnumerable<Imagen> Imagens  { get; }
    }
}
