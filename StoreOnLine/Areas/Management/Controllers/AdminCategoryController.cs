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
    public class AdminCategoryController : Controller
    {
        private readonly ICategoryRepository _repository;
        private readonly IImagenRepository _imagenRepository;

        //Management/AdminCategory
        public AdminCategoryController(ICategoryRepository repo, IImagenRepository repoImagen)
        {
            ViewBag.Big = "Admin: Categorias";
            ViewBag.Small = "Configuracion de Categorias";
            ViewBag.Area = "Management";
            ViewBag.Controller = "AdminCategory";
            ViewBag.View = "Index";
            _repository = repo;
            _imagenRepository = repoImagen;
        }

        public ActionResult Index()
        {
            var db = _repository.Categories.Where(o => !o.IsDeleted);
            var view = db.Select(category => new CategoryView().ToView(category)).ToList();
            return View(view);
        }

        public ViewResult Edit(int categoryId)
        {
            ViewBag.View = "Edit";
            var category = _repository.Categories.FirstOrDefault(p => p.Id == categoryId);
            if (category != null) ViewBag.Category = category.CategoryName;

            return View(new CategoryView().ToView(category));
        }

        [HttpPost]
        public ActionResult Edit(CategoryView model, HttpPostedFileBase imagen = null)
        {
            if (ModelState.IsValid)
            {
                if (imagen != null)
                {
                    var map = new Bitmap(Image.FromStream(imagen.InputStream), new Size(150, 150));
                    var newImagen = new Imagen();
                    newImagen.ImageData = Util.Img.ImgTransform.ConvertBitMapToByteArray(map);
                    newImagen.ImageMimeType = ImageFormat.Jpeg.ToString();
                    newImagen.ObjectName = model.CategoryName;
                    newImagen.ObjectId = model.Id;
                    newImagen.IsPrincipal = true;
                    imagen.InputStream.Read(newImagen.ImageData, 0, imagen.ContentLength);
                    _repository.SaveCategory(model.ToBd(model, newImagen));
                }
                else
                {
                    var firstOrDefault = _repository.Categories.FirstOrDefault(p => p.Id == model.Id);
                    if (firstOrDefault != null && firstOrDefault.CategoryPhoto != null)
                    {
                        firstOrDefault.CategoryPhoto.ObjectName = model.CategoryName;
                        firstOrDefault.CategoryPhoto.ObjectId = model.Id;
                        _repository.SaveCategory(model.ToBd(model, firstOrDefault.CategoryPhoto));
                    }
                    _repository.SaveCategory(model.ToBd(model, null));
                }

                TempData["message"] = string.Format("{0} ha sido guardado", model.CategoryName);
                return RedirectToAction("Index");
            }
            // there is something wrong with the data values
            return View(model);
        }


        public ActionResult Create()
        {
            return View("Edit", new CategoryView());
        }


        public ActionResult Delete(int categoryId)
        {
            var deletedCategory = _repository.DeleteCategory(categoryId);
            if (deletedCategory != null)
            {
                TempData["message"] = string.Format("{0} fue eliminado", deletedCategory.CategoryName);
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