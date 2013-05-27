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
            var projetos = db.Projetos.Include(p => p.Cliente).Include(p => p.Status).Include(p => p.Usuario);
            return View(projetos.ToList());
        }

    }
}
