using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreOnLine.Areas.Merchant.Controllers
{
    public class AssignController : Controller
    {
        //
        // GET: /Merchant/Assign/
        public ActionResult Index(int companyId)
        {
            return View();
        }

        //
        // GET: /Merchant/Assign/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Merchant/Assign/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Merchant/Assign/Create
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
        // GET: /Merchant/Assign/Edit/5
        public ActionResult Edit(int companyId)
        {
            return View();
        }

        //
        // POST: /Merchant/Assign/Edit/5
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
        // GET: /Merchant/Assign/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Merchant/Assign/Delete/5
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
