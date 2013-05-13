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
    public class TarefaController : Controller
    {
        private MHCAEntities db = new MHCAEntities();

        //
        // GET: /Tarefa/

        public ActionResult Index()
        {
            var tarefas = db.Tarefas.Include(t => t.Projeto).Include(t => t.Status).Include(t => t.Usuario);
            return View(tarefas.ToList());
        }

        //
        // GET: /Tarefa/Details/5

        public ActionResult Details(int id = 0)
        {
            Tarefa tarefa = db.Tarefas.Find(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            return View(tarefa);
        }

        //
        // GET: /Tarefa/Create

        public ActionResult Create()
        {
            ViewBag.Projeto_Id = new SelectList(db.Projetos, "Id", "Nome");
            ViewBag.Status_Id = new SelectList(db.Status, "Id", "Nome");
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome");
            return View();
        }

        //
        // POST: /Tarefa/Create

        [HttpPost]
        public ActionResult Create(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                db.Tarefas.Add(tarefa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Projeto_Id = new SelectList(db.Projetos, "Id", "Nome", tarefa.Projeto_Id);
            ViewBag.Status_Id = new SelectList(db.Status, "Id", "Nome", tarefa.Status_Id);
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome", tarefa.Usuario_Id);
            return View(tarefa);
        }

        //
        // GET: /Tarefa/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Tarefa tarefa = db.Tarefas.Find(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Projeto_Id = new SelectList(db.Projetos, "Id", "Nome", tarefa.Projeto_Id);
            ViewBag.Status_Id = new SelectList(db.Status, "Id", "Nome", tarefa.Status_Id);
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome", tarefa.Usuario_Id);
            return View(tarefa);
        }

        //
        // POST: /Tarefa/Edit/5

        [HttpPost]
        public ActionResult Edit(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarefa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Projeto_Id = new SelectList(db.Projetos, "Id", "Nome", tarefa.Projeto_Id);
            ViewBag.Status_Id = new SelectList(db.Status, "Id", "Nome", tarefa.Status_Id);
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome", tarefa.Usuario_Id);
            return View(tarefa);
        }

        //
        // GET: /Tarefa/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Tarefa tarefa = db.Tarefas.Find(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            return View(tarefa);
        }

        //
        // POST: /Tarefa/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Tarefa tarefa = db.Tarefas.Find(id);
            db.Tarefas.Remove(tarefa);
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