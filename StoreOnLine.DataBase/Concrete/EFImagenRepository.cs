using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Resources;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace StoreOnLine.DataBase.Concrete
{
    public  class ImagenRepository:IImagenRepository
    {
        private readonly StoreOnLineContext _context = new StoreOnLineContext();

        public IEnumerable<Imagen> Imagens
        {
            get { return _context.Imagens.Where(o => !o.IsDeleted); }
        }

        public int SaveImagen(Imagen imagen)
        {
            if (imagen.Id == 0)
            {
                _context.Imagens.Add(imagen);
            }
            else
            {
                var dbEntry = _context.Imagens.Find((imagen.Id));
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(imagen))
                {
                    if (!property.Name.Equals("Id") && property.GetValue(imagen) != null)
                    {
                        var value = property.GetValue(imagen);
                        if (value != null) property.SetValue(dbEntry, value);
                    }
                }

            }
            _context.SaveChanges();
            return imagen.Id;
        }

        public Imagen DeleteImagen(int imagenId, bool physical = false)
        {
            var dbEntry = _context.Imagens.Find(imagenId);
            if (dbEntry != null)
            {
                if (physical)
                {
                    _context.Imagens.Remove(dbEntry);
                }
                else
                {
                    dbEntry.IsDeleted = true;
                    dbEntry.Active = false;
                    _context.Entry(dbEntry).State = EntityState.Modified;
                }

                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
