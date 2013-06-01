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
    [Authorize]
    [MMC2.Filters.InitializeSimpleMembership]
    public class HistoricoController : Controller
    {
        private MHCAEntities db = new MHCAEntities();

        //
        // GET: /Historico/

        public ActionResult Index()
        {
            var historicos = db.Historicos.Include(h => h.Tarefa).Include(h => h.Usuario);
            return View(historicos.ToList());
        }

        //
        // GET: /Historico/Details/5

        public ActionResult Details(int id = 0)
        {
            Historico historico = db.Historicos.Find(id);
            if (historico == null)
            {
                return HttpNotFound();
            }
            return View(historico);
        }

        //
        // GET: /Historico/Create

        public ActionResult Create()
        {
            ViewBag.Tarefa_Id = new SelectList(db.Tarefas, "Id", "Nome");
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome");
            return View();
        }

        //
        // POST: /Historico/Create

        [HttpPost]
        public ActionResult Create(Historico historico)
        {
            if (ModelState.IsValid)
            {
                historico.Usuario_Id = (int)Session["-USUARIO"];
                db.Historicos.Add(historico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tarefa_Id = new SelectList(db.Tarefas, "Id", "Nome", historico.Tarefa_Id);
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome", historico.Usuario_Id);
            return View(historico);
        }

        //
        // GET: /Historico/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Historico historico = db.Historicos.Find(id);
            if (historico == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tarefa_Id = new SelectList(db.Tarefas, "Id", "Nome", historico.Tarefa_Id);
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome", historico.Usuario_Id);
            return View(historico);
        }

        //
        // POST: /Historico/Edit/5

        [HttpPost]
        public ActionResult Edit(Historico historico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tarefa_Id = new SelectList(db.Tarefas, "Id", "Nome", historico.Tarefa_Id);
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome", historico.Usuario_Id);
            return View(historico);
        }

        //
        // GET: /Historico/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Historico historico = db.Historicos.Find(id);
            if (historico == null)
            {
                return HttpNotFound();
            }
            return View(historico);
        }

        //
        // POST: /Historico/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Historico historico = db.Historicos.Find(id);
            db.Historicos.Remove(historico);
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