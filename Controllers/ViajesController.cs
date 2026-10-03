using System;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using SGTB;

namespace SGTB.Controllers
{
    public class ViajesController : Controller
    {
        private SistemaTransporteDBEntities db = new SistemaTransporteDBEntities();

        
        public ActionResult Index(int? id)
        {
            try
            {
                ViewBag.ListaRutas = db.Rutas.ToList();
                ViewBag.ListaBuses = db.Buses.ToList();
                ViewBag.ListaConductores = db.Conductores.ToList();

                
                ViewBag.ListaViajes = db.Viajes
                    .Include("Rutas")
                    .Include("Buses")
                    .Include("Conductores")
                    .ToList();

                if (id.HasValue)
                {
                    var viajeEditar = db.Viajes.Find(id.Value);
                    return View(viajeEditar);
                }

                return View();
            }
            catch
            {
                ViewBag.ListaViajes = db.Viajes.ToList();
                return View();
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guardar(int Id, int RutaId, int BusId, int ConductorId, DateTime Fecha, TimeSpan HoraSalida, decimal Precio, int AsientosDisponibles)
        {
            try
            {
                if (Id == 0)
                {
                    
                    var nuevoViaje = new Viajes
                    {
                        RutaId = RutaId,
                        BusId = BusId,
                        ConductorId = ConductorId,
                        Fecha = Fecha,
                        HoraSalida = HoraSalida,
                        Precio = Precio,
                        AsientosDisponibles = AsientosDisponibles
                    };
                    db.Viajes.Add(nuevoViaje);
                }
                else
                {
                    
                    var viajeExistente = db.Viajes.Find(Id);
                    if (viajeExistente != null)
                    {
                        viajeExistente.RutaId = RutaId;
                        viajeExistente.BusId = BusId;
                        viajeExistente.ConductorId = ConductorId;
                        viajeExistente.Fecha = Fecha;
                        viajeExistente.HoraSalida = HoraSalida;
                        viajeExistente.Precio = Precio;
                        viajeExistente.AsientosDisponibles = AsientosDisponibles;
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
                var viaje = db.Viajes.Find(id);
                if (viaje != null)
                {
                    db.Viajes.Remove(viaje);
                    db.SaveChanges();
                }
            }
            catch { }

            return RedirectToAction("Index");
        }

        public ActionResult Consultas(int? rutaId, int? busId, int? conductorId, DateTime? fecha)
        {
            try
            {
                ViewBag.ListaRutas = db.Rutas.ToList();
                ViewBag.ListaBuses = db.Buses.ToList();
                ViewBag.ListaConductores = db.Conductores.ToList();

                var query = db.Viajes
                    .Include("Rutas")
                    .Include("Buses")
                    .Include("Conductores")
                    .AsQueryable();

                if (rutaId.HasValue && rutaId.Value > 0)
                {
                    query = query.Where(v => v.RutaId == rutaId.Value);
                }

                if (busId.HasValue && busId.Value > 0)
                {
                    query = query.Where(v => v.BusId == busId.Value);
                }

                if (conductorId.HasValue && conductorId.Value > 0)
                {
                    query = query.Where(v => v.ConductorId == conductorId.Value);
                }

                if (fecha.HasValue)
                {
                    query = query.Where(v => v.Fecha == fecha.Value);
                }

                ViewBag.ListaViajes = query.ToList();
            }
            catch
            {
                ViewBag.ListaViajes = db.Viajes.ToList();
            }

            return View();
        }
    }
}
