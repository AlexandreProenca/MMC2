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
    public class TipoSkillController : Controller
    {
        private MHCAEntities db = new MHCAEntities();

        //
        // GET: /TipoSkill/

        public ActionResult Index()
        {
            return View(db.TipoSkills.ToList());
        }

        //
        // GET: /TipoSkill/Details/5

        public ActionResult Details(int id = 0)
        {
            TipoSkill tiposkill = db.TipoSkills.Find(id);
            if (tiposkill == null)
            {
                return HttpNotFound();
            }
            return View(tiposkill);
        }

        //
        // GET: /TipoSkill/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TipoSkill/Create

        [HttpPost]
        public ActionResult Create(TipoSkill tiposkill)
        {
            if (ModelState.IsValid)
            {
                db.TipoSkills.Add(tiposkill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tiposkill);
        }

        //
        // GET: /TipoSkill/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TipoSkill tiposkill = db.TipoSkills.Find(id);
            if (tiposkill == null)
            {
                return HttpNotFound();
            }
            return View(tiposkill);
        }

        //
        // POST: /TipoSkill/Edit/5

        [HttpPost]
        public ActionResult Edit(TipoSkill tiposkill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiposkill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tiposkill);
        }

        //
        // GET: /TipoSkill/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TipoSkill tiposkill = db.TipoSkills.Find(id);
            if (tiposkill == null)
            {
                return HttpNotFound();
            }
            return View(tiposkill);
        }

        //
        // POST: /TipoSkill/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoSkill tiposkill = db.TipoSkills.Find(id);
            db.TipoSkills.Remove(tiposkill);
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