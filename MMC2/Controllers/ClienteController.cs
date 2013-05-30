using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using MMC2.Models;

namespace MMC2.Controllers
{
    public class ClienteController : Controller
    {
        private MHCAEntities db = new MHCAEntities();

        //
        // GET: /Cliente/

        public ActionResult Index()
        {
            var clientes = (from a in db.Clientes where a.Ativo == true orderby a.Nome select a).ToList();
            return View(clientes);
        }

        //
        // GET: /Cliente/Details/5

        public ActionResult Details(int id = 0)
        {
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        public PartialViewResult GridCliente(string nome)
        {
            var clientes = (from a in db.Clientes where a.Ativo == true && a.Nome.Contains(nome) orderby a.Nome select a).ToList();
            return PartialView(clientes);
        }

        //public ActionResult GetClientes(string data)
        //{
        //    var clientes = (from a in db.Clientes where a.Ativo == true && a.Nome.Contains(data) orderby a.Nome select a).ToList();
        //    return View(clientes);
        //}

        //
        // GET: /Cliente/Create

        public ActionResult Create()
        {
            //ViewBag.Endereco_Id = new SelectList(db.Enderecos, "Id", "Rua");
            return View();
        }

        public ActionResult Relatorio(int _id = 0)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Report/ReportRelatorioProjeto.rdlc");
            var obj = (from a in db.vw_relatorio_projeto where a.projeto_id == _id select new { 
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
                a.historico_id });
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

        public ActionResult CreateEndereco()
        {
            return View();
        }

        //
        // POST: /Cliente/Create

        [HttpPost]
        public ActionResult Create(SuperModel sm)
        {
            if (ModelState.IsValid)
            {
                sm.Cliente.DataHora = DateTime.Now;
                sm.Cliente.Ativo = true;
                //Endereco ed = new Endereco();
                //ed = SM.EnderecoNovo;
                sm.Cliente.Enderecos.Add(sm.EnderecoNovo);
                db.Clientes.Add(sm.Cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.Endereco_Id = new SelectList(db.Enderecos, "Id", "Rua", cliente.Endereco_Id);
            return View(sm);
        }

        [HttpPost]
        public ActionResult CreateEndereco(Endereco endereco, int idCliente)
        {
            if (ModelState.IsValid)
            {
                endereco.Cliente_id = idCliente;
                db.Enderecos.Add(endereco);
                db.SaveChanges();
                return RedirectToAction("Edit/" + endereco.Cliente_id.Value.ToString());
            }

            //ViewBag.Endereco_Id = new SelectList(db.Enderecos, "Id", "Rua", cliente.Endereco_Id);
            return View(endereco);
        }

        //
        // GET: /Cliente/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SuperModel sm = new SuperModel();
            Cliente cliente = (from a in db.Clientes where a.Id == id select a).FirstOrDefault();
            sm.Cliente = cliente;
            //sm.lstEndereco.AddRange(from a in db.Enderecos select a);
            sm.Endereco = cliente.Enderecos.OrderBy(a => a.Rua);
            //sm.lstEndereco.AddRange(sm.Endereco);
                //db.Clientes.Find(id);
            if (sm == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Endereco_Id = new SelectList(db.Enderecos, "Id", "Rua", sm.Cliente.Endereco_Id);
            return View(sm);
        }

        public ActionResult EditEndereco(int id = 0)
        {
            Endereco endereco = (from a in db.Enderecos where a.Id == id select a).FirstOrDefault();
            if (endereco == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Endereco_Id = new SelectList(db.Enderecos, "Id", "Rua", sm.Cliente.Endereco_Id);
            return View(endereco);
        }

        //
        // POST: /Cliente/Edit/5

        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Endereco_Id = new SelectList(db.Enderecos, "Id", "Rua", cliente.Endereco_Id);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult EditEndereco(Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(endereco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit/"+endereco.Cliente_id.Value.ToString());
            }
            //ViewBag.Endereco_Id = new SelectList(db.Enderecos, "Id", "Rua", cliente.Endereco_Id);
            return View(endereco);
        }

        //
        // GET: /Cliente/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        //
        // POST: /Cliente/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult DeleteEndereco(int id = 0)
        {
            Endereco endereco = db.Enderecos.Find(id);
            if (endereco == null)
            {
                return HttpNotFound();
            }
            return View(endereco);
        }

        //
        // POST: /Cliente/Delete/5

        [HttpPost, ActionName("DeleteEndereco")]
        public ActionResult DeleteEnderecoConfirmed(int id)
        {
            Endereco endereco = db.Enderecos.Find(id);
            int idCliente = endereco.Cliente_id.Value;
            db.Enderecos.Remove(endereco);
            db.SaveChanges();
            return RedirectToAction("Edit/" + idCliente.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}