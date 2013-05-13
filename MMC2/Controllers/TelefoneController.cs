using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMC2.Models;

namespace MMC2.Controllers
{
    public class TelefoneController : Controller
    {
        private MHCAEntities db = new MHCAEntities();

        //
        // GET: /Telefone/

        public ActionResult Index()
        {
            var telefones = db.Telefones.Include(t => t.Endereco);
            return View(telefones.ToList());
        }

        //
        // GET: /Telefone/Details/5

        public ActionResult Details(int id = 0)
        {
            Telefone telefone = db.Telefones.Find(id);
            if (telefone == null)
            {
                return HttpNotFound();
            }
            return View(telefone);
        }

        //
        // GET: /Telefone/Create

        public ActionResult Create()
        {
            ViewBag.Endereco_Id = new SelectList(db.Enderecos, "Id", "Rua");
            return View();
        }

        //
        // POST: /Telefone/Create

        [HttpPost]
        public ActionResult Create(Telefone telefone)
        {
            if (ModelState.IsValid)
            {
                db.Telefones.Add(telefone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Endereco_Id = new SelectList(db.Enderecos, "Id", "Rua", telefone.Endereco_Id);
            return View(telefone);
        }

        //
        // GET: /Telefone/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Telefone telefone = db.Telefones.Find(id);
            if (telefone == null)
            {
                return HttpNotFound();
            }
            ViewBag.Endereco_Id = new SelectList(db.Enderecos, "Id", "Rua", telefone.Endereco_Id);
            return View(telefone);
        }

        //
        // POST: /Telefone/Edit/5

        [HttpPost]
        public ActionResult Edit(Telefone telefone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telefone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Endereco_Id = new SelectList(db.Enderecos, "Id", "Rua", telefone.Endereco_Id);
            return View(telefone);
        }

        //
        // GET: /Telefone/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Telefone telefone = db.Telefones.Find(id);
            if (telefone == null)
            {
                return HttpNotFound();
            }
            return View(telefone);
        }

        //
        // POST: /Telefone/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Telefone telefone = db.Telefones.Find(id);
            db.Telefones.Remove(telefone);
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