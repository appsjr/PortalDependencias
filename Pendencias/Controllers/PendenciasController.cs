using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pendencias.Models;
using System.Threading;
using Pendencias.CustomAttributes;

namespace Pendencias.Controllers
{
    public class PendenciasController : Controller
    {
        private DTOPendenciaContext db = new DTOPendenciaContext();
        private DBPendenciasEntities dbLogin = new DBPendenciasEntities();

        // GET: Pendencias
        [NoDirectAccess]
        public ActionResult Index()
        {
            return View(ObtenhaPendencias().ToList());
        }

        public ActionResult AtualizaListaDePendencias()
        {
            return View("_ListaDePendencias", ObtenhaPendencias().ToList());
        }

        // GET: Pendencias/Details/5
        [NoDirectAccess]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DTOPendencia dTOPendencia = db.DTOPendencias.Find(id);
            if (dTOPendencia == null)
            {
                return HttpNotFound();
            }
            return View(dTOPendencia);
        }

        // GET: Pendencias/Create
        [NoDirectAccess]
        public ActionResult Create()
        {
            IniciaViewBag();
            return View();
        }

        private void IniciaViewBag()
        {
            ViewBag.Categorias = db.DTOCategorias.ToList();
            ViewBag.Usuarios = dbLogin.Database.SqlQuery<Usuario>(@"Select * from login").ToList();
        }

        // POST: Pendencias/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoDirectAccess]
        public ActionResult Create([Bind(Include = "ID,Titulo,Descricao,Status,Responsavel,Obs,Data,CategoriaID")] DTOPendencia dTOPendencia)
        {
            if (ModelState.IsValid)
            {
                dTOPendencia.ID = Guid.NewGuid();
                dTOPendencia.Data = DateTime.Now;
                db.DTOPendencias.Add(dTOPendencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dTOPendencia);
        }

        // GET: Pendencias/Edit/5
        [NoDirectAccess]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DTOPendencia dTOPendencia = db.DTOPendencias.Find(id);
            if (dTOPendencia == null)
            {
                return HttpNotFound();
            }
            IniciaViewBag();
            return View(dTOPendencia);
        }

        // POST: Pendencias/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoDirectAccess]
        public ActionResult Edit([Bind(Include = "ID,Titulo,Descricao,Status,Responsavel,Obs,CategoriaID")] DTOPendencia dTOPendencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dTOPendencia).State = EntityState.Modified;
                dTOPendencia.Data = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            IniciaViewBag();
            return View(dTOPendencia);
        }

        // GET: Pendencias/Delete/5
        [NoDirectAccess]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DTOPendencia dTOPendencia = db.DTOPendencias.Find(id);
            if (dTOPendencia == null)
            {
                return HttpNotFound();
            }
            return View(dTOPendencia);
        }

        // POST: Pendencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [NoDirectAccess]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DTOPendencia dTOPendencia = db.DTOPendencias.Find(id);
            db.DTOPendencias.Remove(dTOPendencia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private DbSet<DTOPendencia> ObtenhaPendencias()
        {
            return db.DTOPendencias;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
