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
    public class SkillController : Controller
    {
        private MHCAEntities db = new MHCAEntities();

        //
        // GET: /Skill/

        public ActionResult Index()
        {
            var skills = db.Skills.Include(s => s.Usuario);
            return View(skills.ToList());
        }

        //
        // GET: /Skill/Details/5

        public ActionResult Details(int id = 0)
        {
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        //
        // GET: /Skill/Create

        public ActionResult Create()
        {
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome");
            return View();
        }

        //
        // POST: /Skill/Create

        [HttpPost]
        public ActionResult Create(Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Skills.Add(skill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome", skill.Usuario_Id);
            return View(skill);
        }

        //
        // GET: /Skill/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome", skill.Usuario_Id);
            return View(skill);
        }

        //
        // POST: /Skill/Edit/5

        [HttpPost]
        public ActionResult Edit(Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Usuario_Id = new SelectList(db.Usuarios, "Id", "Nome", skill.Usuario_Id);
            return View(skill);
        }

        //
        // GET: /Skill/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        //
        // POST: /Skill/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Skill skill = db.Skills.Find(id);
            db.Skills.Remove(skill);
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