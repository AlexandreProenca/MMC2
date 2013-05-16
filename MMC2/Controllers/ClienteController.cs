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
    public class ClienteController : Controller
    {
        private MHCAEntities db = new MHCAEntities();

        //
        // GET: /Cliente/

        public ActionResult Index()
        {
            var clientes = (from a in db.Clientes where a.Ativo == true select a).ToList();
            return View(clientes);
        }

        //
        // GET: /Cliente/Details/5

        public ActionResult Details(int id = 0)
        {
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        //
        // GET: /Cliente/Create

        public ActionResult Create()
        {
            ViewBag.Endereco_Id = new SelectList(db.Enderecos, "Id", "Rua");
            return View();
        }

        //
        // POST: /Cliente/Create

        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.DataHora = DateTime.Now;
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.Endereco_Id = new SelectList(db.Enderecos, "Id", "Rua", cliente.Endereco_Id);
            return View(cliente);
        }

        //
        // GET: /Cliente/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SuperModel sm = new SuperModel();
            Cliente cliente = (from a in db.Clientes where a.Id == id select a).FirstOrDefault();
            sm.Cliente = cliente;
            //sm.lstEndereco.AddRange(from a in db.Enderecos select a);
            sm.Endereco = cliente.Enderecos;
            //sm.lstEndereco.AddRange(sm.Endereco);
                //db.Clientes.Find(id);
            if (sm == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Endereco_Id = new SelectList(db.Enderecos, "Id", "Rua", sm.Cliente.Endereco_Id);
            return View(sm);
        }

        //
        // POST: /Cliente/Edit/5

        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Endereco_Id = new SelectList(db.Enderecos, "Id", "Rua", cliente.Endereco_Id);
            return View(cliente);
        }

        //
        // GET: /Cliente/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        //
        // POST: /Cliente/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
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