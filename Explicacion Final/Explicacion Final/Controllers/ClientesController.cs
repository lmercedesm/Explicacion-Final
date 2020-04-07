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
    public class ClientesController : Controller
    {
        private SistemaDeFacturacionEntities1 db = new SistemaDeFacturacionEntities1();

        // GET: Clientes
        public ActionResult Index(string Nombre, string Categoria)
        {
            //ViewBag.Cliente = new SelectList(db.Clientes,"Categoria");
            var Cliente = from d in db.Clientes
                          select d;
            if (!string.IsNullOrEmpty(Nombre))
            {
                Cliente = Cliente.Where(n => n.Nombre.Contains(Nombre));
            }
            if (!string.IsNullOrEmpty(Categoria))
            {
                Cliente = Cliente.Where(n => n.Categoria.Contains(Categoria));
            }
            return View(Cliente);
            //return View(db.Clientes.ToList());
        }


        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.DNI = DNI();
            ViewBag.Categoria = Categoria();
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCliente,DNI,Nombre,Telefono,Email,Categoria")] Cliente cliente)
        {

            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
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
        public List<SelectListItem> Categoria()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text="Premium",
                    Value="Premium",

                },
                new SelectListItem()
                {
                    Text="Regular",
                    Value="Regular",

                },

            };
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCliente,DNI,Nombre,Telefono,Email,Categoria")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
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
