using StoreOnLine.Areas.Merchant.Models;
using StoreOnLine.Areas.Security.Models;
using StoreOnLine.DataBase.Abstract;
using System.Web.Mvc;

namespace StoreOnLine.Areas.Merchant.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICompanyRepository _repo;

        public AdminController(ICompanyRepository repo)
        {
            ViewBag.Big = "Clientes: Compañia";
            ViewBag.Small = "Lista de Compañias registradas";
            ViewBag.Area = "Merchant";
            ViewBag.Controller = "Admin";
            ViewBag.Action = "Index";
            _repo = repo;
        }

        //
        // GET: /Merchant/Admin/
        public ActionResult Index()
        {
            var db = _repo.Companies;
            object view = null;//db.Select(company => new CompanyView().ToView(company)).ToList();
            return View(view);
        }

        //
        // GET: /Merchant/Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Merchant/Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Merchant/Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Merchant/Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Merchant/Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Merchant/Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Merchant/Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
