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
    public class TarefaController : Controller
    {
        private MHCAEntities db = new MHCAEntities();

        //
        // GET: /Tarefa/

        public ActionResult Index()
        {
            var tarefas = db.Tarefas.Include(t => t.Projeto).Include(t => t.Status).Include(t => t.TipoSkill).Include(t => t.Usuario);
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
            ViewBag.Habilidade_Id = new SelectList(db.TipoSkills, "Id", "Nome");
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

                //if((string)Session["-PAPEL"] == "Desenvolvedor")
                //{

                Usuario user = db.Usuarios.Find((int)Session["-USUARIO"]);
                tarefa.Usuario = user;
                //}
                tarefa.Status_Id = 2; //inicializa com status ativo
                tarefa.Porcentagem = 0;
                db.Tarefas.Add(tarefa);
                db.SaveChanges();

                TempData["habilidade"] = tarefa.Habilidade_Id;

                return RedirectToAction("../Recurso");//preciso passa o objeto tarefa junto com tmpDATA
            }

            ViewBag.Projeto_Id = new SelectList(db.Projetos, "Id", "Nome", tarefa.Projeto_Id);
            ViewBag.Status_Id = new SelectList(db.Status, "Id", "Nome", tarefa.Status_Id);
            ViewBag.Habilidade_Id = new SelectList(db.TipoSkills, "Id", "Nome", tarefa.Habilidade_Id);
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome", tarefa.Usuario_Id);

            return RedirectToAction("Index");
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
            ViewBag.Habilidade_Id = new SelectList(db.TipoSkills, "Id", "Nome", tarefa.Habilidade_Id);
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
                if (tarefa.Porcentagem == 100)
                {
                    //recupero um obj do tipo skill para adicionar xp
                    Skill obj = (from a in db.Skills
                                 where a.Usuario_Id == tarefa.Usuario_Id 
                                 select a).FirstOrDefault();
                    //var obj = (from a in db.Skills
                    //           where a.TipoSkills_Id.Equals(tarefa.Habilidade_Id)
                    //           select a).FirstOrDefault();
                    obj.Nivel += 1;
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();

                    tarefa.Status_Id = 3; //tarefa finalizado


                }
                db.Entry(tarefa).State = EntityState.Modified;
                db.SaveChanges();

                Usuario user = db.Usuarios.Find((int)Session["-USUARIO"]);

                if (user.TipoUsuario == "Gerente" || user.TipoUsuario == "Administrador")
                {
                    return RedirectToAction("Index");
                }
                else
                {

                    return RedirectToAction("../Dashuser/Create");
                }
            }
            ViewBag.Projeto_Id = new SelectList(db.Projetos, "Id", "Nome", tarefa.Projeto_Id);
            ViewBag.Status_Id = new SelectList(db.Status, "Id", "Nome", tarefa.Status_Id);
            ViewBag.Habilidade_Id = new SelectList(db.TipoSkills, "Id", "Nome", tarefa.Habilidade_Id);
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