using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreOnLine.Areas.Merchant.Controllers
{
    public class AssistController : Controller
    {
        //
        // GET: /Merchant/Assist/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Merchant/Assist/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Merchant/Assist/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Merchant/Assist/Create
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
        // GET: /Merchant/Assist/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Merchant/Assist/Edit/5
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
        // GET: /Merchant/Assist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Merchant/Assist/Delete/5
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
