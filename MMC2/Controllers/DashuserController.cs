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
    public class DashuserController : Controller
    {
        private MHCAEntities db = new MHCAEntities();

        //
        // GET: /Dashuser/

        public ActionResult Index()
        {
            var historicos = db.Historicos.Include(h => h.Tarefa).Include(h => h.Usuario);
            return View(historicos.ToList());
        }

        public PartialViewResult GridTarefa()
        {

            int usuario_id = Convert.ToInt32(Session["-USUARIO"]);
            var tarefas = (from a in db.Tarefas where a.Porcentagem != 100 && a.Usuario_Id == usuario_id orderby a.Nome select a).ToList();
            return PartialView("_Tarefas", tarefas);
        }

        public PartialViewResult GridHistorico()
        {
            int usuario_id = Convert.ToInt32(Session["-USUARIO"]);

            var historicos = (from a in db.Historicos where a.Tarefa.Usuario_Id == usuario_id orderby a.DataLancamento select a).ToList();
            return PartialView("_Historicos", historicos);
        }

        public JsonResult GetGauge()
        {
            int usuario_id = Convert.ToInt32(Session["-USUARIO"]);

            string entrega = (from a in db.Tarefas where a.Porcentagem == 100 && a.Usuario_Id == usuario_id select a).Count().ToString();
            string ativas = (from a in db.Tarefas where a.Usuario_Id == usuario_id && a.Porcentagem != 100 select a).Count().ToString();
            string atrasadas = (from a in db.Tarefas where a.DataFinal < DateTime.Now && a.Usuario_Id == usuario_id && a.Porcentagem != 100 select a).Count().ToString();

            List<string[]> data = new List<string[]>();
            data.Add(new[] { "", "Value" });
            data.Add(new[] { "Entregues", entrega });
            data.Add(new[] { "Atrasadas", atrasadas });
            data.Add(new[] { "Em ativas", ativas });


            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Dashuser/Details/5

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
        // GET: /Dashuser/Create

        public ActionResult Create()
        {
            int usuario_id = (int)Session["-USUARIO"];
            var obj = (from a in db.Tarefas where a.Usuario_Id == usuario_id && a.Porcentagem != 100 select new { a.Id, a.Nome }).ToList();
            ViewBag.Tar = obj;

            ViewBag.Tarefa_Id = new SelectList(db.Tarefas, "Id", "Nome");
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome");
            return View();
        }

        //
        // POST: /Dashuser/Create

        [HttpPost]
        public ActionResult Create(Historico historico)
        {
            int usuario_id = (int)Session["-USUARIO"];
            if (ModelState.IsValid)
            {

                
                historico.Usuario_Id = usuario_id;
                db.Historicos.Add(historico);
                db.SaveChanges();
                return RedirectToAction("Create");
            }



            var obj = (from a in db.Tarefas where a.Usuario_Id == usuario_id select new { a.Id, a.Nome }).ToList();
            ViewBag.Tar = obj;

            ViewBag.Tarefa_Id = new SelectList(db.Tarefas, "Id", "Nome", historico.Tarefa_Id);
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome", historico.Usuario_Id);
            return View(historico);
        }

        //
        // GET: /Dashuser/Edit/5

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
        // POST: /Dashuser/Edit/5

        [HttpPost]
        public ActionResult Edit(Historico historico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create");
            }
            

            ViewBag.Tarefa_Id = new SelectList(db.Tarefas, "Id", "Nome", historico.Tarefa_Id);
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome", historico.Usuario_Id);
            return View(historico);
        }

        //
        // GET: /Dashuser/Delete/5

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
        // POST: /Dashuser/Delete/5

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