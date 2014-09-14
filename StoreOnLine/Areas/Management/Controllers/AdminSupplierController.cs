using System.Linq;
using System.Web.Mvc;
using StoreOnLine.Areas.Management.Models;
using StoreOnLine.DataBase.Abstract;

namespace StoreOnLine.Areas.Management.Controllers
{
    [Authorize]
    public class AdminSupplierController : Controller
    {
        private readonly ISupplierRepository _repository;

        public AdminSupplierController(ISupplierRepository repo)
        {
            ViewBag.Big = "Administrar: Proveedores";
            ViewBag.Small = "Configuracion de los proveedores de productos";
            ViewBag.Area = "Management";
            ViewBag.Controller = "AdminSupplier";
            ViewBag.Action = "Index";
            _repository = repo;
        }
        //
        // GET: /Management/AdminSupplier/
        public ActionResult Index()
        {
            var db = _repository.Suppliers;
            var view = db.Select(productBase => new SupplierView().ToView(productBase)).ToList();
            return View(view);
        }

        public ViewResult Create()
        {
            return View("Edit", new SupplierView { IsStatus = true });
        }

        public ViewResult Edit(int supplierId)
        {
            ViewBag.Action = "Edit";

            var supplier = _repository.Suppliers.FirstOrDefault(p => p.Id == supplierId);
            if (supplier != null) ViewBag.Supplier = supplier.SupplierName;

            return View(new SupplierView().ToView(supplier));
        }

        [HttpPost]
        public ActionResult Edit(SupplierView supplier)
        {
            if (!ModelState.IsValid) return View(supplier);

            _repository.SaveSupplier(supplier.ToBd(supplier));
            TempData["message"] = string.Format("{0} has been saved", supplier.SupplierName);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int supplierId)
        {
            var deletedSupplier = _repository.DeleteSupplier(supplierId);
            if (deletedSupplier != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedSupplier.SupplierName);
            }
            return RedirectToAction("Index");
        }
    }
}