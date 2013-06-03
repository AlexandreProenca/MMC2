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
    public class DashbordController : Controller
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

      
       

        public JsonResult GetAreaChartData()
        {

            int projeto_id = Convert.ToInt32(Session["-IDPROJETO"]);

            string ativas = (from a in db.Tarefas where a.Status_Id == 2 && a.Projeto_Id == projeto_id select a).Count().ToString();
            string impedida = (from a in db.Tarefas where a.Status_Id == 4 && a.Projeto_Id == projeto_id select a).Count().ToString();
            string corrente = (from a in db.Tarefas where a.Status_Id == 5 && a.Projeto_Id == projeto_id  select a).Count().ToString();
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
            int projeto_id = Convert.ToInt32(Session["-IDPROJETO"]);

            string entrega = (from a in db.Tarefas where a.Porcentagem == 100 && a.Projeto_Id == projeto_id select a).Count().ToString();
            string totalTarefas = (from a in db.Tarefas where a.Projeto_Id == projeto_id select a).Count().ToString();
            string atrasadas = (from a in db.Tarefas where a.DataFinal < DateTime.Now && a.Projeto_Id == projeto_id select a).Count().ToString();

            List<string[]> data = new List<string[]>();
            data.Add(new[] { "", "Value" });
            data.Add(new[] { "Finalizada", entrega });
            data.Add(new[] { "Atrasadas", atrasadas });
            data.Add(new[] { "Tarefas", totalTarefas });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GridProjeto()
        {
            int gerente_id = Convert.ToInt32(Session["-USUARIO"]);
            var projetos = (from a in db.Projetos where a.Status_Id == 2 && a.Gerente_Id == gerente_id orderby a.Nome select a).ToList();
            Projeto obj= (from a in db.Projetos where a.Status_Id == 2 && a.Gerente_Id == gerente_id select a).FirstOrDefault();
            Session["-IDPROJETO"] = obj.Id; 
            return PartialView("_Projetos", projetos);
        }

        public PartialViewResult GridTarefa()
        {
            int projeto = Convert.ToInt32(Session["-IDPROJETO"]);
            var tarefas = (from a in db.Tarefas where a.Status_Id == 5 && a.Projeto_Id == projeto orderby a.Nome select a).ToList();
            return PartialView("_Tarefas", tarefas);
        }

        public PartialViewResult GridTarefaConcuida()
        {
            int projeto = Convert.ToInt32(Session["-IDPROJETO"]);
            var tarefas = (from a in db.Tarefas where a.Porcentagem == 100 && a.Projeto_Id == projeto orderby a.Nome select a).ToList();
            return PartialView("_TarefasConcuidas", tarefas);
        }

        public PartialViewResult GridHistorico()
        {  
            int projeto = Convert.ToInt32(Session["-IDPROJETO"]);
            var historicos = (from a in db.Historicos where a.Tarefa.Projeto_Id == projeto orderby a.DataLancamento select a).ToList();
            return PartialView("_Historicos", historicos);
        }

    }
}
