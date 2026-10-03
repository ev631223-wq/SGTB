using System.Linq;
using System.Web.Mvc;
using SGTB;

namespace SGTB.Controllers
{
    public class ConductoresController : Controller
    {
        private SistemaTransporteDBEntities db = new SistemaTransporteDBEntities();

        
        public ActionResult Index(int? id)
        {
            try
            {
                
                if (id.HasValue)
                {
                    var conductorEditar = db.Conductores.Find(id.Value);
                    ViewBag.ListaConductores = db.Conductores.ToList();
                    return View(conductorEditar);
                }

                ViewBag.ListaConductores = db.Conductores.ToList();
                return View();
            }
            catch
            {
                ViewBag.ListaConductores = null;
                return View();
            }
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guardar(int Id, string Cedula, string Nombre, string Licencia, string Telefono, string Estado)
        {
            try
            {
                if (Id == 0)
                {
                    
                    var nuevoConductor = new Conductores
                    {
                        Cedula = Cedula,
                        Nombre = Nombre,
                        Licencia = Licencia,
                        Telefono = Telefono,
                        Estado = Estado
                    };
                    db.Conductores.Add(nuevoConductor);
                }
                else
                {
                    
                    var conductorExistente = db.Conductores.Find(Id);
                    if (conductorExistente != null)
                    {
                        conductorExistente.Cedula = Cedula;
                        conductorExistente.Nombre = Nombre;
                        conductorExistente.Licencia = Licencia;
                        conductorExistente.Telefono = Telefono;
                        conductorExistente.Estado = Estado;
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
                var conductor = db.Conductores.Find(id);
                if (conductor != null)
                {
                   
                    var viajesAsociados = db.Viajes.Where(v => v.ConductorId == id).ToList();
                    db.Viajes.RemoveRange(viajesAsociados);

                    
                    db.Conductores.Remove(conductor);
                    db.SaveChanges();
                }
            }
            catch { }

            return RedirectToAction("Index");
        }

    }
}