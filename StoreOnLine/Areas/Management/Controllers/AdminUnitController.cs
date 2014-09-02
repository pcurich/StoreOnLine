using StoreOnLine.Areas.Management.Models;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Const;
using StoreOnLine.DataBase.Model.Resources;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreOnLine.Areas.Management.Controllers
{
    public class AdminUnitController : Controller
    {
        private readonly IUnitRepository _repository;
        private readonly IImagenRepository _imagenRepository;

        //Management/AdminUnit/
        public AdminUnitController(IUnitRepository repo, IImagenRepository repoImagen)
        {
            ViewBag.Big = "Admin: Unidades";
            ViewBag.Small = "Configuracion de unidades de medidas";
            ViewBag.Area = "Management";
            ViewBag.Controller = "AdminUnit";
            ViewBag.Action = "Index";
            _repository = repo;
            _imagenRepository = repoImagen;
        }

        public ActionResult Index()
        {
            var db = _repository.Units.Where(o => !o.IsDeleted);
            var view = db.Select(category => new UnitView().ToView(category)).ToList();
            return View(view);
        }

        public ViewResult Edit(int unitId)
        {
            ViewBag.Action = "Edit";
            var unit = _repository.Units.FirstOrDefault(p => p.Id == unitId);
            if (unit != null) ViewBag.Unit = unit.UnitName;

            return View(new UnitView().ToView(unit));
        }

        [HttpPost]
        public ActionResult Edit(UnitView model, HttpPostedFileBase imagen = null)
        {
            if (ModelState.IsValid)
            {
                if (imagen != null)
                {
                    var map = new Bitmap(Image.FromStream(imagen.InputStream), new Size(150, 150));
                    var newImagen =
                        _imagenRepository.Imagens
                        .SingleOrDefault(i => i.ObjectName == model.UnitName &&
                                              i.ObjectId == model.Id && i.IsPrincipal);
                    if (newImagen != null)
                    {
                        newImagen.IsPrincipal = false;
                        _imagenRepository.SaveImagen(newImagen);
                    }
                    newImagen = new Imagen();
                    newImagen.ImageData = Util.Img.ImgTransform.ConvertBitMapToByteArray(map);
                    newImagen.ImageMimeType = ImageFormat.Jpeg.ToString();
                    newImagen.ObjectName = model.UnitName;
                    newImagen.ObjectId = model.Id;
                    newImagen.IsPrincipal = true;
                    imagen.InputStream.Read(newImagen.ImageData, 0, imagen.ContentLength);
                    _repository.SaveUnit(model.ToBd(model, newImagen));
                }
                else
                {
                    var firstOrDefault = _repository.Units.FirstOrDefault(p => p.Id == model.Id);
                    if (firstOrDefault != null && firstOrDefault.UnitPhoto != null)
                    {
                        firstOrDefault.UnitPhoto.ObjectName = model.UnitName;
                        firstOrDefault.UnitPhoto.ObjectId = model.Id;
                        _repository.SaveUnit(model.ToBd(model, firstOrDefault.UnitPhoto));
                    }
                    _repository.SaveUnit(model.ToBd(model, null));
                }

                TempData["message"] = string.Format("{0} ha sido guardado", model.UnitName);
                return RedirectToAction("Index");
            }
            // there is something wrong with the data values
            return View(model);
        }

        public ActionResult Create()
        {
            return View("Edit", new UnitView());
        }

        public ActionResult Delete(int unitId)
        {
            var deletedCategory = _repository.DeleteUnit(unitId);
            if (deletedCategory != null)
            {
                TempData["message"] = string.Format("{0} fue eliminado", deletedCategory.UnitName);
            }
            return RedirectToAction("Index");
        }

        public FileContentResult GetImage(String name, int id)
        {
            var imagen =
                _imagenRepository.Imagens.FirstOrDefault(p => p.ObjectName == name && p.ObjectId == id && p.IsPrincipal) ??
                _imagenRepository.Imagens.FirstOrDefault(p => p.ObjectName == ObjectName.Default);
            return imagen != null ? File(imagen.ImageData, imagen.ImageMimeType) : null;
        }
    }
}