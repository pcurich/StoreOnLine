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
    public class AdminCampaignController : Controller
    {
        private readonly ICampaingRepository _repository;
        private readonly IImagenRepository _imagenRepository;

        //Management/AdminCampaign/
        public AdminCampaignController(ICampaingRepository repo, IImagenRepository repoImagen)
        {
            ViewBag.Big = "Admin: Campañas";
            ViewBag.Small = "Configuracion de Campañas";
            ViewBag.Area = "Management";
            ViewBag.Controller = "AdminCategory";
            ViewBag.Action = "Index";
            _repository = repo;
            _imagenRepository = repoImagen;
        }

        public ActionResult Index()
        {
            var db = _repository.Campaigns.Where(o => !o.IsDeleted);
            var view = db.Select(category => new CampaignView().ToView(category)).ToList();
            return View(view);
        }

        public ViewResult Edit(int campaignId)
        {
            ViewBag.Action = "Edit";
            var campaign = _repository.Campaigns.FirstOrDefault(p => p.Id == campaignId);
            if (campaign != null) ViewBag.Campaign = campaign.CampaignName;

            return View(new CampaignView().ToView(campaign));
        }

        [HttpPost]
        public ActionResult Edit(CampaignView model, HttpPostedFileBase imagen = null)
        {
            if (ModelState.IsValid)
            {
                if (imagen != null)
                {
                    var map = new Bitmap(Image.FromStream(imagen.InputStream), new Size(150, 150));
                    var newImagen =
                        _imagenRepository.Imagens
                        .SingleOrDefault(i => i.ObjectName == model.CampaignName &&
                                              i.ObjectId == model.Id && i.IsPrincipal);
                    if (newImagen != null)
                    {
                        newImagen.IsPrincipal = false;
                        _imagenRepository.SaveImagen(newImagen);
                    }

                    newImagen = new Imagen();
                    newImagen.ImageData = Util.Img.ImgTransform.ConvertBitMapToByteArray(map);
                    newImagen.ImageMimeType = ImageFormat.Jpeg.ToString();
                    newImagen.ObjectName = model.CampaignName;
                    newImagen.ObjectId = model.Id;
                    newImagen.IsPrincipal = true;
                    imagen.InputStream.Read(newImagen.ImageData, 0, imagen.ContentLength);
                    _repository.SaveCampaign(model.ToBd(model, newImagen));

                }
                else
                {
                    var firstOrDefault = _repository.Campaigns.FirstOrDefault(p => p.Id == model.Id);
                    if (firstOrDefault != null && firstOrDefault.CampaignPhoto != null)
                    {
                        firstOrDefault.CampaignPhoto.ObjectName = model.CampaignName;
                        firstOrDefault.CampaignPhoto.ObjectId = model.Id;
                        _repository.SaveCampaign(model.ToBd(model, firstOrDefault.CampaignPhoto));
                    }
                    _repository.SaveCampaign(model.ToBd(model, null));
                }

                TempData["message"] = string.Format("{0} ha sido guardado", model.CampaignName);
                return RedirectToAction("Index");
            }
            // there is something wrong with the data values
            return View(model);
        }

        public ActionResult Create()
        {
            return View("Edit", new CampaignView());
        }

        public ActionResult Delete(int campaignId)
        {
            var deletedCategory = _repository.DeleteCampaign(campaignId);
            if (deletedCategory != null)
            {
                TempData["message"] = string.Format("{0} fue eliminado", deletedCategory.CampaignName);
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