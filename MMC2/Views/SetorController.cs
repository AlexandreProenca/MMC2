using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMC2.Models;

namespace MMC2.Views
{
    public class SetorController : Controller
    {
        private MHCAEntities db = new MHCAEntities();

        //
        // GET: /Setor/

        public ActionResult Index()
        {
            return View(db.Setores.ToList());
        }

        //
        // GET: /Setor/Details/5

        public ActionResult Details(int id = 0)
        {
            Setore setore = db.Setores.Find(id);
            if (setore == null)
            {
                return HttpNotFound();
            }
            return View(setore);
        }

        //
        // GET: /Setor/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Setor/Create

        [HttpPost]
        public ActionResult Create(Setore setore)
        {
            if (ModelState.IsValid)
            {
                db.Setores.Add(setore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(setore);
        }

        //
        // GET: /Setor/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Setore setore = db.Setores.Find(id);
            if (setore == null)
            {
                return HttpNotFound();
            }
            return View(setore);
        }

        //
        // POST: /Setor/Edit/5

        [HttpPost]
        public ActionResult Edit(Setore setore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(setore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(setore);
        }

        //
        // GET: /Setor/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Setore setore = db.Setores.Find(id);
            if (setore == null)
            {
                return HttpNotFound();
            }
            return View(setore);
        }

        //
        // POST: /Setor/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Setore setore = db.Setores.Find(id);
            db.Setores.Remove(setore);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}