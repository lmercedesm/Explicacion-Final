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
    public class StocksController : Controller
    {
        private SistemaDeFacturacionEntities1 db = new SistemaDeFacturacionEntities1();

        // GET: Stocks
        public ActionResult Index(string Proveedor, string Producto)
        {
            var Entradas = from d in db.Stocks
                           select d;
            if (!string.IsNullOrEmpty(Proveedor))
            {
                Entradas = Entradas.Where(n => n.Proveedor.Contains(Proveedor));
            }
            if (!string.IsNullOrEmpty(Producto))
            {
                Entradas = Entradas.Where(n => n.Producto.Contains(Producto));
            }
            return View(Entradas);
        }

        // GET: Stocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            List<StockViewModels> stocks = null;
            using (Models.SistemaDeFacturacionEntities1 db = new Models.SistemaDeFacturacionEntities1())
            {
                stocks = (from d in db.Productos
                          select new StockViewModels
                          {

                              Producto = d.Nombre,
                              IdProducto = d.IdProductos
                          }).ToList();

            }
            List<SelectListItem> productos = stocks.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Producto.ToString(),
                    Value = d.Producto.ToString(),
                    Selected = false
                };
            });

            //Lista Proveedores
            List<StockViewModels> stock = null;
            using (Models.SistemaDeFacturacionEntities1 db = new Models.SistemaDeFacturacionEntities1())
            {
                stock = (from d in db.Proveedores
                         select new StockViewModels
                         {

                             Proveedor = d.Nombre,
                             IdProveedor = d.IdProveedores
                         }).ToList();

            }
            List<SelectListItem> proveedores = stock.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Proveedor.ToString(),
                    Value = d.Proveedor.ToString(),
                    Selected = false
                };
            });

            ViewBag.productos = productos;
            ViewBag.proveedores = proveedores;
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdStock,Producto,Cantidad,Proveedor,Fecha")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Stocks.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stock);
        }

        // GET: Stocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdStock,Producto,Cantidad,Proveedor,Fecha")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock stock = db.Stocks.Find(id);
            db.Stocks.Remove(stock);
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
