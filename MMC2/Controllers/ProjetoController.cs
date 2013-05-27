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
    public class ProjetoController : Controller
    {
        private MHCAEntities db = new MHCAEntities();

        //
        // GET: /Projeto/

        public ActionResult Index()
        {
            var projetos = db.Projetos.Include(p => p.Cliente).Include(p => p.Status).Include(p => p.Usuario);
            return View(projetos.ToList());
        }

        //
        // GET: /Projeto/Details/5

        public ActionResult Details(int id = 0)
        {
            Projeto projeto = db.Projetos.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }

        //
        // GET: /Projeto/Create

        public ActionResult Create()
        {

            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nome");
            ViewBag.Status_Id = new SelectList(db.Status, "Id", "Nome");
            ViewBag.Gerente_Id = new SelectList(db.Usuarios, "Id", "Nome");
            return View();
        }

        //
        // POST: /Projeto/Create

        [HttpPost]
        public ActionResult Create(Projeto projeto)
        {
            if (ModelState.IsValid)
            {
                projeto.Gerente_Id = (int)Session["-USUARIO"]; // O projeto eh criado em nome de quem estiver logado
                projeto.Status_Id = 2; //incializa o projeto como ativo
                db.Projetos.Add(projeto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nome", projeto.Cliente_Id);
            ViewBag.Status_Id = new SelectList(db.Status, "Id", "Nome", projeto.Status_Id);
            ViewBag.Gerente_Id = new SelectList(db.Usuarios, "Id", "Nome", projeto.Gerente_Id);
            return View(projeto);
        }

        //
        // GET: /Projeto/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Projeto projeto = db.Projetos.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nome", projeto.Cliente_Id);
            ViewBag.Status_Id = new SelectList(db.Status, "Id", "Nome", projeto.Status_Id);
            ViewBag.Gerente_Id = new SelectList(db.Usuarios, "Id", "Nome", projeto.Gerente_Id);
            return View(projeto);
        }

        //
        // POST: /Projeto/Edit/5

        [HttpPost]
        public ActionResult Edit(Projeto projeto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projeto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nome", projeto.Cliente_Id);
            ViewBag.Status_Id = new SelectList(db.Status, "Id", "Nome", projeto.Status_Id);
            ViewBag.Gerente_Id = new SelectList(db.Usuarios, "Id", "Nome", projeto.Gerente_Id);
            return View(projeto);
        }

        //
        // GET: /Projeto/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Projeto projeto = db.Projetos.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }

        //
        // POST: /Projeto/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Projeto projeto = db.Projetos.Find(id);
            db.Projetos.Remove(projeto);
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