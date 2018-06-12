using Pendencias.CustomAttributes;
using Pendencias.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Pendencias.Controllers
{
    public class CategoriasController : Controller
    {
        DTOPendenciaContext db = new DTOPendenciaContext();

        // GET: Categorias
        [NoDirectAccess]
        public ActionResult Index()
        {
            return View(db.DTOCategorias.ToList());
        }

        // GET: Categorias/Details/5
        [NoDirectAccess]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Categorias/Create
        [NoDirectAccess]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        [HttpPost]
        [NoDirectAccess]
        public ActionResult Create([Bind(Include = "Descricao,Codigo")] DTOCategoria dtoCategoria)
        {
            try
            {
                // TODO: Add insert logic here
                db.DTOCategorias.Add(dtoCategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categorias/Edit/5
        [NoDirectAccess]
        public ActionResult Edit(int id)
        {
            return View(db.DTOCategorias.Where(x => x.Codigo == id).First());
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoDirectAccess]
        public ActionResult Edit([Bind(Include = "Descricao,Codigo")] DTOCategoria dtoCategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dtoCategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dtoCategoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            return View(db.DTOCategorias.Where(x => x.Codigo == id).First());
        }

        // POST: Categorias/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                DTOCategoria categoria = db.DTOCategorias.Find(id);
                db.DTOCategorias.Remove(categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
