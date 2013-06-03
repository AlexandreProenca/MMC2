
using System.Data;
using System.Data.Entity;
using MMC2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMC2.Controllers
{
    public class UserDashController : Controller
    {
        //
        // GET: /Dashbord/

        private MHCAEntities db = new MHCAEntities();

        //
        // GET: /Projeto/

        public ActionResult Index()
        {

            return View();
        }


        public ActionResult DefineTarefa(int id = 0)
        {
            Tarefa tarefa = db.Tarefas.Find(id);

            return View("AddTarefa",tarefa);
        }

        public JsonResult GetAreaChartData()
        {

            int projeto_id = Convert.ToInt32(Session["-IDPROJETO"]);

            string ativas = (from a in db.Tarefas where a.Status_Id == 2 && a.Projeto_Id == projeto_id select a).Count().ToString();
            string impedida = (from a in db.Tarefas where a.Status_Id == 4 && a.Projeto_Id == projeto_id select a).Count().ToString();
            string corrente = (from a in db.Tarefas where a.Status_Id == 5 && a.Projeto_Id == projeto_id select a).Count().ToString();
            string encerradas = (from a in db.Tarefas where a.Status_Id == 3 && a.Projeto_Id == projeto_id select a).Count().ToString();

            List<string[]> data = new List<string[]>();
            data.Add(new[] { "Label", "Value" });
            data.Add(new[] { "Impedidas", impedida });
            data.Add(new[] { "Finalizadas", encerradas });
            data.Add(new[] { "Ativas", ativas });
            data.Add(new[] { "Em andamento", corrente });

            return Json(data, JsonRequestBehavior.AllowGet);
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



        public PartialViewResult GridTarefa()
        {

            int usuario_id = Convert.ToInt32(Session["-USUARIO"]);
            var tarefas = (from a in db.Tarefas where a.Porcentagem != 100 && a.Usuario_Id == usuario_id orderby a.Nome select a).ToList();
            return PartialView("_Tarefas", tarefas);
        }

        public PartialViewResult FormHistorico()
        {

            int usuario_id = Convert.ToInt32(Session["-USUARIO"]);
            Historico historico = (from a in db.Historicos where a.Tarefa.Usuario_Id == usuario_id orderby a.DataLancamento select a).FirstOrDefault();
            Tarefa tarefa = (from a in db.Tarefas where a.Porcentagem != 100 && a.Usuario_Id == usuario_id orderby a.Nome select a).FirstOrDefault();
            historico.Tarefa = tarefa;
            return PartialView("_FormHistorico", historico);
        }

        public PartialViewResult GridHistorico()
        {
            int usuario_id = Convert.ToInt32(Session["-USUARIO"]);

            var historicos = (from a in db.Historicos where a.Tarefa.Usuario_Id == usuario_id orderby a.DataLancamento select a).ToList();
            return PartialView("_Historicos", historicos);
        }


        public ActionResult AddTarefa()
        {
            ViewBag.Tarefa_Id = new SelectList(db.Tarefas, "Id", "Nome");
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome");
            
            return View();
        }

        //
        // POST: /Historico/Create

        [HttpPost]
        public ActionResult AddTarefa(Historico historico)
        {
           int usuario_id = (int)Session["-USUARIO"];


            if (ModelState.IsValid)
            {
                //historico.Tarefa_Id = 
                historico.Usuario_Id = usuario_id;
                db.Historicos.Add(historico);
                db.SaveChanges();
                return View("_Historicos", historico);
            }

            ViewBag.Tarefa_Id = new SelectList(db.Tarefas, "Id", "Nome", historico.Tarefa_Id);
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome", historico.Usuario_Id);
            return View();
        }

    }
}
