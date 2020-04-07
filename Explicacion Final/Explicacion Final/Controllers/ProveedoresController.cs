using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Explicacion_Final.Models;
using Rotativa;

namespace Explicacion_Final.Controllers
{
    public class ProveedoresController : Controller
    {
        private SistemaDeFacturacionEntities1 db = new SistemaDeFacturacionEntities1();

        // GET: Proveedores
        public ActionResult Index(string Nombre, string Email)
        {
            var Proveedores = from d in db.Proveedores
                              select d;
            if (!string.IsNullOrEmpty(Nombre))
            {
                Proveedores = Proveedores.Where(n => n.Nombre.Contains(Nombre));
            }
            if (!string.IsNullOrEmpty(Email))
            {
                Proveedores = Proveedores.Where(n => n.Email.Contains(Email));
            }
            return View(Proveedores);
        }

        // GET: Proveedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedore proveedore = db.Proveedores.Find(id);
            if (proveedore == null)
            {
                return HttpNotFound();
            }
            return View(proveedore);
        }

        // GET: Proveedores/Create
        public ActionResult Create()
        {
            ViewBag.DNI = DNI();
            return View();
        }
        public List<SelectListItem> DNI()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text="Cedula",
                    Value="Cedula",

                },
                new SelectListItem()
                {
                    Text="RNC",
                    Value="RNC",

                },

            };
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProveedores,TipoDNI,DNI,Nombre,Telefono,Email")] Proveedore proveedore)
        {
            if (ModelState.IsValid)
            {
                db.Proveedores.Add(proveedore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proveedore);
        }

        // GET: Proveedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedore proveedore = db.Proveedores.Find(id);
            if (proveedore == null)
            {
                return HttpNotFound();
            }
            return View(proveedore);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProveedores,DNI,Nombre,Telefono,Email")] Proveedore proveedore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proveedore);
        }

        // GET: Proveedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedore proveedore = db.Proveedores.Find(id);
            if (proveedore == null)
            {
                return HttpNotFound();
            }
            return View(proveedore);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proveedore proveedore = db.Proveedores.Find(id);
            db.Proveedores.Remove(proveedore);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Print()
        {

            return new ActionAsPdf("Index")
            { FileName = "Reporte.pdf" };
        }
    }
}
