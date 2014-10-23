using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Const;
using System.Linq;
using System.Web.Mvc;

namespace StoreOnLine.Areas.Management.Controllers
{
    public class AdminImgController : Controller
    {
        private readonly IImagenRepository _repository;

        public AdminImgController(IImagenRepository repo)
        {
            _repository = repo;
        }

        public FileContentResult GetDefault(int id=0)
        {
            if (id == 0)
            {
                var imagen = _repository.Imagens.FirstOrDefault(p => p.ObjectName == ObjectName.Default);
                return imagen != null ? File(imagen.ImageData, imagen.ImageMimeType) : null;
            }
            else
            {
                var imagen = _repository.Imagens.FirstOrDefault(p => p.ObjectId == id);
                return imagen != null ? File(imagen.ImageData, imagen.ImageMimeType) : null;
            }
        }


        public FileContentResult GetImageProductBase(int id, int secuence = 0)
        {
            var imagen =
                _repository.Imagens.FirstOrDefault(p => p.ObjectId == id && p.Secuence==secuence) ??
                _repository.Imagens.FirstOrDefault(p => p.ObjectName == ObjectName.Default && p.Secuence==secuence);
            return imagen != null ? File(imagen.ImageData, imagen.ImageMimeType) : null;
        }

        public FileContentResult GetImageDefault(string defaultName)
        {
            var imagen =
                _repository.Imagens.FirstOrDefault(p => p.ObjectName == defaultName && p.ObjectId==0) ??
                _repository.Imagens.FirstOrDefault(p => p.ObjectName == ObjectName.Default );
            return imagen != null ? File(imagen.ImageData, imagen.ImageMimeType) : null;
        }
    }
}