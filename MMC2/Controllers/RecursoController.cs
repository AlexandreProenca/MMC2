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
    public class RecursoController : Controller
    {
        private MHCAEntities db = new MHCAEntities();

        //
        // GET: /Recurso/

        public ActionResult Index()
        {
            int habilidade = (int)TempData["habilidade"];
            //var obj = db.Skills.Include(s => s.Usuario).Include(s => s.TipoSkill);
            var obj = (from a in db.Skills.Include(s => s.Usuario).Include(s => s.TipoSkill)
                       where a.TipoSkills_Id.Equals(habilidade)
                       select a);

            return View(obj.ToList());
        }

        //
        // GET: /action para amarrar o usuario com a tarefa

        public ActionResult DefineUsuario(int id = 0)
        {

            Skill skill = db.Skills.Find(id);

            Tarefa tar = (from a in db.Tarefas
                          orderby a.Id descending
                          select a).FirstOrDefault();

            if (skill == null || tar == null)
            {
                return HttpNotFound();
            }

            tar.Usuario = skill.Usuario;
            db.Entry(tar).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("../Tarefa");
        }

        //
        // GET: /Recurso/Create

        public ActionResult Create()
        {
            ViewBag.Projeto_Id = new SelectList(db.Projetos, "Id", "Nome");
            ViewBag.Status_Id = new SelectList(db.Status, "Id", "Nome");
            ViewBag.Habilidade_Id = new SelectList(db.TipoSkills, "Id", "Nome");
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome");
            return View();
        }

        //
        // POST: /Recurso/Create

        //[HttpPost]
        //public ActionResult Create (int id = 0)
        //{

        //        //Usuario user = db.Usuarios.Find((int)TempData["id_user"]);
        //        Usuario user = db.Usuarios.Find(id);

        //        //int id = TempData["id_tarefa"] = tarefa.Id;
        //        //Usuario usuario = db.Usuario.Find(usuario_id);
        //       // Tarefa tarefa = db.Tarefas.Find(id);
        //        Tarefa tar = (from a in db.Tarefas select a).FirstOrDefault();

        //        tar.Usuario = user;// amarra o usuario escolhido com a tarefa criada
        //        db.Entry(tar).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return View(tar);


        //}

        //
        // GET: /Recurso/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Tarefa tarefa = db.Tarefas.Find(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Projeto_Id = new SelectList(db.Projetos, "Id", "Nome", tarefa.Projeto_Id);
            ViewBag.Status_Id = new SelectList(db.Status, "Id", "Nome", tarefa.Status_Id);
            ViewBag.Habilidade_Id = new SelectList(db.TipoSkills, "Id", "Nome", tarefa.Habilidade_Id);
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome", tarefa.Usuario_Id);
            return View(tarefa);
        }

        //
        // POST: /Recurso/Edit/5

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
            ViewBag.Habilidade_Id = new SelectList(db.TipoSkills, "Id", "Nome", tarefa.Habilidade_Id);
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome", tarefa.Usuario_Id);
            return View(tarefa);
        }

        //
        // GET: /Recurso/Delete/5

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
        // POST: /Recurso/Delete/5

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

        public object Habilidade { get; set; }
    }
}