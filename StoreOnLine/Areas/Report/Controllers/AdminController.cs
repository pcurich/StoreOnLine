using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StoreOnLine.Areas.Report.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Report/Admin/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Report/Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Report/Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Report/Admin/Create
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
        // GET: /Report/Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Report/Admin/Edit/5
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
        // GET: /Report/Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Report/Admin/Delete/5
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
