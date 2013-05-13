using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using MMC2.Models;
namespace MMC2.Controllers
{
    public class LoginController : Controller
    {
        private MHCAEntities db = new MHCAEntities();

        //
        // GET: /Acesso/

        public ActionResult Logar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logon(Usuario usuario, string returnUrl)
        {
            var obj = (from a in db.Usuarios where a.Email.ToLower().Equals(usuario.Email) && a.Senha.Equals(usuario.Senha) select a).FirstOrDefault();
            if (ModelState.IsValid && obj != null)
            {
                Session["-USUARIO"] = obj.Id;
                return RedirectToAction(returnUrl);
            }

            //ViewBag.Endereco_Id
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "O e-mail ou a senha estão incorretos.");
            return View(usuario);
        }
        
        //
        // GET: /Acesso/Lembrar

        public ActionResult Lembrar()
        {
            return View();
        }

    }
}
