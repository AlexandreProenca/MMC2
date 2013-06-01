using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using MMC2.Models;

namespace MMC2.Controllers
{
    [Authorize]
    [MMC2.Filters.InitializeSimpleMembership]
    public class RelatorioController : Controller
    {
        //
        // GET: /Relatorio/


        private MHCAEntities db = new MHCAEntities();

        public ActionResult Index(int projetoid = 0)
        {
            var obj = (from a in db.Projetos select new { a.Id, a.Nome }).ToList();
            ViewBag.Projeto = obj;
            Session["-IDPROJETO"] = projetoid;
            return View();
        }

        public ActionResult Relatorio(int _id = 0)
        {
            _id = Convert.ToInt32(Session["-IDPROJETO"]);
            if (_id > 0)
            {
                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Report/ReportRelatorioProjeto.rdlc");
                var obj = (from a in db.vw_relatorio_projeto
                           where a.projeto_id == _id
                           select new
                           {
                               a.projeto_id,
                               a.projeto,
                               a.DescricaoProjeto,
                               a.cliente,
                               a.DtIniProjeto,
                               a.DtFimProjeto,
                               a.tarefa,
                               a.DescricaoTarefa,
                               a.tarefa_id,
                               a.colaborador,
                               a.status,
                               a.datalancamento,
                               a.qtdhoras,
                               a.DescricaoHistorico,
                               a.historico_id
                           });
                ReportDataSource reportDataSource = new ReportDataSource("DataSet1", obj);
                localReport.DataSources.Add(reportDataSource);

                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                //The DeviceInfo settings should be changed based on the reportType
                //http://msdn2.microsoft.com/en-us/library/ms155397.aspx
                string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                //Render the report
                renderedBytes = localReport.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);
                //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension);
                return File(renderedBytes, mimeType);
            }
            else
            {
                return HttpNotFound();
            }
        }

    }
}
