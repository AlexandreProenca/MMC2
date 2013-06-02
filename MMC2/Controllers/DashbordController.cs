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

        //[HttpPost]
        //public JsonResult GetAreaChartData()
        //{
          

        //    var data = new[]
        //        {
        //            new {Name = "Status", Value = "Tarefa"},
        //            new {Name = "Ativas", Value = 10},
        //            new {Name = "Finalizadas", Value = 20},
        //            new {Name = "Canceladas", Value = 25},
        //            new {Name = "Em andamento", Value = 45},
        //        };

        //    return Json(data,JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetDataAssets()
        {
            List<object> data = new List<object>();
            data.Add(new[]  { "Status", "Tarefa" });
            data.Add(new[] { 1, 11 });
            data.Add(new[] {2, 2 });
            data.Add(new[] {3, 2 });
             data.Add(new[] {4, 2});
             data.Add(new[] { 5, 7});
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
       

        public JsonResult GetAreaChartData()
        {


            string ativas = (from a in db.Tarefas where a.Status_Id == 2 select a).Count().ToString();
            string impedida = (from a in db.Tarefas where a.Status_Id == 4 select a).Count().ToString();
            string corrente = (from a in db.Tarefas where a.Status_Id == 5 select a).Count().ToString();

            List<string[]> data = new List<string[]>();
            data.Add(new[] { "Label", "Value" });
            data.Add(new[] { "Impedidas", impedida });
            data.Add(new[] { "Canceladas", "2" });
            data.Add(new[] { "Ativas", ativas });
            data.Add(new[] { "Em andamento", corrente });
           
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetGauge()
        {
            //var obj = (string)(from a in db.Tarefas select a).Count();
            string entrega = (from a in db.Tarefas where a.Porcentagem == 100 select a).Count().ToString();
            String totalTarefas = (from a in db.Tarefas select a).Count().ToString();
            string atrasadas = (from a in db.Tarefas where a.DataFinal < DateTime.Now select a).Count().ToString();

            List<string[]> data = new List<string[]>();
            data.Add(new[] { "", "Value" });
            data.Add(new[] { "Finalizada", entrega });
            data.Add(new[] { "Atrasadas", atrasadas });
            data.Add(new[] { "Tarefas", totalTarefas });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GridProjeto()
        {
            var projetos = (from a in db.Projetos where a.Status_Id == 2 orderby a.Nome select a).ToList();
            return PartialView("_Projetos", projetos);
        }

        public PartialViewResult GridTarefa()
        {
            var tarefas = (from a in db.Tarefas where a.Porcentagem != 100 orderby a.Nome select a).ToList();
            return PartialView("_Tarefas", tarefas);
        }

        public PartialViewResult GridHistorico()
        {
            var historicos = (from a in db.Historicos orderby a.DataLancamento select a).ToList();
            return PartialView("_Historicos", historicos);
        }

    }
}
