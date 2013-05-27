using System.Data;
using System.Data.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using MMC2.Filters;
using MMC2.Models;

namespace MMC2.Controllers
{
    public class UsuarioController : Controller
    {
        private MHCAEntities db = new MHCAEntities();

        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Setore);
            return View(usuarios.ToList().Where(a => a.Ativo == true).OrderBy(a => a.Nome));
        }

        //
        // GET: /Usuario/Details/5

        public ActionResult Details(int id = 0)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //
        // GET: /Usuario/Create

        public ActionResult Create()
        {
            ViewBag.Setor_Id = new SelectList(db.Setores, "Id", "Nome");
            return View();
        }

        //
        // POST: /Usuario/Create

        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(usuario.Email, usuario.Senha);
                    WebSecurity.Login(usuario.Email, usuario.Senha);
                    
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
                usuario.Ativo = true;
                usuario.DataHora = DateTime.Now;
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Setor_Id = new SelectList(db.Setores, "Id", "Nome", usuario.Setor_Id);
            return View(usuario);
        }

        private string ErrorCodeToString(MembershipCreateStatus membershipCreateStatus)
        {
            throw new NotImplementedException();
        }

        //
        // GET: /Usuario/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Setor_Id = new SelectList(db.Setores, "Id", "Nome", usuario.Setor_Id);
            return View(usuario);
        }

        //
        // POST: /Usuario/Edit/5

        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Setor_Id = new SelectList(db.Setores, "Id", "Nome", usuario.Setor_Id);
            return View(usuario);
        }

        //
        // GET: /Usuario/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //
        // POST: /Usuario/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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