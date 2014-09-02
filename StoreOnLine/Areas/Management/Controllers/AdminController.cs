using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Const;
using System.Linq;
using System.Web.Mvc;

namespace StoreOnLine.Areas.Management.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IImagenRepository _repository;

        public AdminController(IImagenRepository repo)
        {
            ViewBag.Big = "Admin";
            ViewBag.Small = "Sistema de Configuracion";
            ViewBag.Area = "Management";
            ViewBag.Controller = "Admin";
            ViewBag.Action = "Index";
            _repository = repo;
        }

        //
        // GET: /Management/Admin/
        public ActionResult Index()
        {
            return View();
        }

        #region Imagen

        public FileContentResult GetImageCategory()
        {
            var imagen = _repository.Imagens
                .FirstOrDefault(p => p.ObjectName == ObjectName.CategoryName && p.IsPrincipal) ?? _repository.Imagens
                    .FirstOrDefault(p => p.ObjectName == ObjectName.Default);
            return imagen != null ? File(imagen.ImageData, imagen.ImageMimeType) : null;
        }

        public FileContentResult GetImageCampaign()
        {
            var imagen = _repository.Imagens
                .FirstOrDefault(p => p.ObjectName == ObjectName.CampaignName && p.IsPrincipal) ?? _repository.Imagens
                    .FirstOrDefault(p => p.ObjectName == ObjectName.Default);
            return imagen != null ? File(imagen.ImageData, imagen.ImageMimeType) : null;
        }

        public FileContentResult GetImageUnit()
        {
            var imagen = _repository.Imagens
                .FirstOrDefault(p => p.ObjectName == ObjectName.UnitName && p.IsPrincipal) ?? _repository.Imagens
                    .FirstOrDefault(p => p.ObjectName == ObjectName.Default);
            return imagen != null ? File(imagen.ImageData, imagen.ImageMimeType) : null;
        }

        public FileContentResult GetImageFeature()
        {
            var imagen = _repository.Imagens
                .FirstOrDefault(p => p.ObjectName == ObjectName.FeatureName && p.IsPrincipal) ?? _repository.Imagens
                    .FirstOrDefault(p => p.ObjectName == ObjectName.Default);
            return imagen != null ? File(imagen.ImageData, imagen.ImageMimeType) : null;
        }

        public FileContentResult GetImageProductComposite()
        {
            var imagen = _repository.Imagens
                .FirstOrDefault(p => p.ObjectName == ObjectName.ProductCompositeName && p.IsPrincipal) ?? _repository.Imagens
                    .FirstOrDefault(p => p.ObjectName == ObjectName.Default);
            return imagen != null ? File(imagen.ImageData, imagen.ImageMimeType) : null;
        }

        public FileContentResult GetImageProductBase()
        {
            var imagen = _repository.Imagens
                .FirstOrDefault(p => p.ObjectName == ObjectName.ProductBaseName && p.IsPrincipal) ?? _repository.Imagens
                    .FirstOrDefault(p => p.ObjectName == ObjectName.Default);
            return imagen != null ? File(imagen.ImageData, imagen.ImageMimeType) : null;
        }

        public FileContentResult GetImageSupplier()
        {
            var imagen = _repository.Imagens
                .FirstOrDefault(p => p.ObjectName == ObjectName.SupplierName && p.IsPrincipal) ?? _repository.Imagens
                    .FirstOrDefault(p => p.ObjectName == ObjectName.Default);
            return imagen != null ? File(imagen.ImageData, imagen.ImageMimeType) : null;
        }
        
        #endregion


    }
}