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

        [HttpPost]
        public JsonResult GetAreaChartData()
        {
            List<string[]> data = new List<string[]>();
            data.Add(new[] { "Tarefas", "Ativos", "Finalizados" });
            data.Add(new[] { "janeiro", "45", "38" });
            data.Add(new[] { "Fevereiro", "33", "15" });
            data.Add(new[] { "Março", "34", "30" });
            data.Add(new[] { "Abril", "33", "15" });

            return this.Json(data);
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
