using System;
using System.Linq;
using System.Web.Mvc;
using SGTB;

namespace SGTB.Controllers
{
    public class BoletosController : Controller
    {
        private SistemaTransporteDBEntities db = new SistemaTransporteDBEntities();


        public ActionResult Index(int? id)
        {
            
            ViewBag.ListaBoletos = db.Boletos.ToList();
            ViewBag.ListaViajes = db.Viajes.ToList();

            if (id.HasValue)
            {
                
                var boletoEditar = db.Boletos.Find(id.Value);
                if (boletoEditar != null)
                {
                    return View(boletoEditar);
                }
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guardar(int Id, int? ViajeId, string NombrePasajero, string CedulaPasajero, string TelefonoPasajero, string EmailPasajero, int AsientoSeleccionado, string MetodoPago, decimal TotalPagar)
        {
            try
            {
                if (Id == 0)
                {
                    
                    var nuevoBoleto = new Boletos
                    {
                        ViajeId = ViajeId,
                        NombrePasajero = NombrePasajero,
                        CedulaPasajero = CedulaPasajero,
                        TelefonoPasajero = TelefonoPasajero,
                        EmailPasajero = EmailPasajero,
                        AsientoSeleccionado = AsientoSeleccionado,
                        MetodoPago = MetodoPago,
                        TotalPagar = TotalPagar,
                        FechaVenta = DateTime.Now
                    };
                    db.Boletos.Add(nuevoBoleto);
                }
                else
                {
                    
                    var boletoExistente = db.Boletos.Find(Id);
                    if (boletoExistente != null)
                    {
                        boletoExistente.ViajeId = ViajeId;
                        boletoExistente.NombrePasajero = NombrePasajero;
                        boletoExistente.CedulaPasajero = CedulaPasajero;
                        boletoExistente.TelefonoPasajero = TelefonoPasajero;
                        boletoExistente.EmailPasajero = EmailPasajero;
                        boletoExistente.AsientoSeleccionado = AsientoSeleccionado;
                        boletoExistente.MetodoPago = MetodoPago;
                        boletoExistente.TotalPagar = TotalPagar;
                    }
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al guardar boleto: " + ex.Message);
            }

            return RedirectToAction("Index");
        }

        
        public ActionResult Eliminar(int id)
        {
            try
            {
                var boleto = db.Boletos.Find(id);
                if (boleto != null)
                {
                    db.Boletos.Remove(boleto);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al eliminar boleto: " + ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}