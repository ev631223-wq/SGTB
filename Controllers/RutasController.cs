using System;
using System.Linq;
using System.Web.Mvc;
using SGTB;

namespace SGTB.Controllers
{
    public class RutasController : Controller
    {
        private SistemaTransporteDBEntities db = new SistemaTransporteDBEntities();

       
        public ActionResult Index(int? id)
        {
            try
            {
                ViewBag.ListaRutas = db.Rutas.ToList();

                if (id.HasValue)
                {
                    var rutaEditar = db.Rutas.Find(id.Value);
                    return View(rutaEditar);
                }

                return View();
            }
            catch
            {
                ViewBag.ListaRutas = db.Rutas.ToList();
                return View();
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guardar(int Id, string Origen, string Destino, string Distancia, string Duracion, string Estado)
        {
            try
            {
                if (Id == 0)
                {
                    
                    var nuevaRuta = new Rutas
                    {
                        Origen = Origen,
                        Destino = Destino,
                        Distancia = Distancia,
                        Duracion = Duracion,
                        Estado = Estado
                    };
                    db.Rutas.Add(nuevaRuta);
                }
                else
                {
                    
                    var rutaExistente = db.Rutas.Find(Id);
                    if (rutaExistente != null)
                    {
                        rutaExistente.Origen = Origen;
                        rutaExistente.Destino = Destino;
                        rutaExistente.Distancia = Distancia;
                        rutaExistente.Duracion = Duracion;
                        rutaExistente.Estado = Estado;
                    }
                }

                db.SaveChanges();
            }
            catch { }

            return RedirectToAction("Index");
        }

        
        public ActionResult Eliminar(int id)
        {
            try
            {
                var ruta = db.Rutas.Find(id);
                if (ruta != null)
                {
                    db.Rutas.Remove(ruta);
                    db.SaveChanges();
                }
            }
            catch { }

            return RedirectToAction("Index");
        }
    }
}