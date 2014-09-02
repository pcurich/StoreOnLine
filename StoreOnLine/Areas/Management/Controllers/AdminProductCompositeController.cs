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
    public class AdminProductCompositeController : Controller
    {
        private readonly IProductsRepository _productRepository;
        private readonly IImagenRepository _imagenRepository;

        //Management/AdminProductComposite/
        public AdminProductCompositeController(IProductsRepository repo, IImagenRepository repoImagen)
        {
            ViewBag.Big = "Admin: Productos Compuestos";
            ViewBag.Small = "Configuracion de Productos Compuestos";
            ViewBag.Area = "Management";
            ViewBag.Controller = "AdminProductComposite";
            ViewBag.Action = "Index";
            _productRepository = repo;
            _imagenRepository = repoImagen;
        }

        public ActionResult Index()
        {
            var db = _productRepository.ProductComposites.Where(o => !o.IsDeleted);
            var view = db.Select(productComposite => new ProductCompositeView().ToView(productComposite)).ToList();
            return View(view);
        }

        public ViewResult Edit(int productCompositeId)
        {
            ViewBag.Action = "Edit";
            var product = _productRepository.ProductComposites.FirstOrDefault(p => p.Id == productCompositeId);
            if (product != null) ViewBag.ProductComposite = product.ProductName;

            return View(new ProductCompositeView().ToView(product));
        }

        [HttpPost]
        public ActionResult Edit(ProductCompositeView model, HttpPostedFileBase imagen = null)
        {
            if (ModelState.IsValid)
            {
                if (imagen != null)
                {
                    var map = new Bitmap(Image.FromStream(imagen.InputStream), new Size(150, 150));
                    var oldImagens =
                        _imagenRepository.Imagens
                        .FirstOrDefault(i => i.ObjectName == model.ProductName &&
                                              i.ObjectId == model.Id && i.IsPrincipal);

                    if (oldImagens != null)
                    {
                        oldImagens.IsPrincipal = false;
                        _imagenRepository.SaveImagen(oldImagens);
                    }

                    var newImagen = new Imagen();
                    newImagen.ImageData = Util.Img.ImgTransform.ConvertBitMapToByteArray(map);
                    newImagen.ImageMimeType = ImageFormat.Jpeg.ToString();
                    newImagen.ObjectName = model.ProductName;
                    newImagen.ObjectId = model.Id;
                    newImagen.IsPrincipal = true;
                    imagen.InputStream.Read(newImagen.ImageData, 0, imagen.ContentLength);

                    var firstOrDefault = _productRepository.ProductComposites.FirstOrDefault(p => p.Id == model.Id);
                    if (firstOrDefault != null)
                    {
                        //Producto existe
                        foreach (var t in firstOrDefault.ProductImagens)
                        {
                            t.IsPrincipal = false;
                        }
                        firstOrDefault.ProductImagens.Add(newImagen);
                        model.ProductImagens = firstOrDefault.ProductImagens;
                        _productRepository.SaveProductComposite(model.ToBd(model));
                    }
                }
                else
                {
                    //No se ingresa Imagen en el productoCompuesto
                    var firstOrDefault = _productRepository.ProductComposites.FirstOrDefault(p => p.Id == model.Id);

                    if (firstOrDefault != null)
                    {
                        model.ProductImagens = firstOrDefault.ProductImagens;
                        _productRepository.SaveProductComposite(model.ToBd(model));
                    }
                }

                TempData["message"] = string.Format("{0} ha sido guardado", model.ProductName);
                return RedirectToAction("Index");
            }
            // there is something wrong with the data values
            return View(model);
        }

        public ActionResult Create()
        {
            return View("Edit", new ProductCompositeView());
        }

        public ActionResult Delete(int productCompositeId)
        {
            var deletedProduct = _productRepository.DeleteProductComposite(productCompositeId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} fue eliminado", deletedProduct.ProductName);
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