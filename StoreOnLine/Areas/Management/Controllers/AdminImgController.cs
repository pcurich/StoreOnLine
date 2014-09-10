using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Model.Resources;
using StoreOnLine.DataBase.Const;

namespace StoreOnLine.Areas.Management.Controllers
{
    public class AdminImgController : Controller
    {
        private readonly IImagenRepository _repository;

        public AdminImgController(IImagenRepository repo)
        {
            _repository = repo;
        }

        public FileContentResult GetDefault()
        {
            var imagen = _repository.Imagens.FirstOrDefault(p => p.ObjectName == ObjectName.Default);
            return imagen != null ? File(imagen.ImageData, imagen.ImageMimeType) : null;
        }


        public FileContentResult GetImageProductBase(int id, int secuence = 0)
        {
            var imagen =
                _repository.Imagens.FirstOrDefault(p => p.ObjectId == id && p.Secuence==secuence) ??
                _repository.Imagens.FirstOrDefault(p => p.ObjectName == ObjectName.Default && p.Secuence==secuence);
            return imagen != null ? File(imagen.ImageData, imagen.ImageMimeType) : null;
        }
    }
}