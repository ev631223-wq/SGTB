using System;
using System.Linq;
using System.Web.Mvc;
using SGTB.Models;

namespace SGTB.Controllers
{
    public class BusesController : Controller
    {
        private SistemaTransporteDBEntities db = new SistemaTransporteDBEntities();

        
        public ActionResult Index(int? id)
        {
            var listaBuses = db.Buses.ToList();
            ViewBag.ListaBuses = listaBuses;

            Buses busModel = new Buses();
            if (id != null)
            {
                busModel = db.Buses.Find(id);
                if (busModel == null)
                {
                    return HttpNotFound();
                }
            }

            return View(busModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guardar(Buses bus)
        {
            if (ModelState.IsValid)
            {
                if (bus.Id == 0) 
                {
                    db.Buses.Add(bus);
                }
                else
                {
                    db.Entry(bus).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ListaBuses = db.Buses.ToList();
            return View("Index", bus);
        }

        
        public ActionResult Eliminar(int id)
        {
            Buses bus = db.Buses.Find(id);
            if (bus != null)
            {
                db.Buses.Remove(bus);
                db.SaveChanges();
            }
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
    }
}