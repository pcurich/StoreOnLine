using System.Collections.Generic;
using StoreOnLine.DataBase.Model.Resources;

namespace StoreOnLine.DataBase.Abstract
{
    public interface IImagenRepository
    {
        IEnumerable<Imagen> Imagens  { get; }
        int SaveImagen(Imagen imagen);
        Imagen DeleteImagen(int imagenId, bool physical = false);
    }
}
