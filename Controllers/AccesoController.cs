using System.Linq;
using System.Web.Mvc;
using SGTB;

namespace SGTB.Controllers
{
    public class AccesoController : Controller
    {
       
        private SistemaTransporteDBEntities db = new SistemaTransporteDBEntities();

        
        public ActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Login(string correo, string clave)
        {
            try
            {
               
                var usuario = db.Usuarios.FirstOrDefault(u => u.Username == correo && u.Password == clave);

                if (usuario != null)
                {
                    Session["Usuario"] = usuario;
                    
                    return RedirectToAction("Index", "Buses");
                }
                else
                {
                    ViewBag.Error = "Usuario o contraseña incorrectos";
                    return View();
                }
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error de conexión: " + ex.Message;
                return View();
            }
        }
    }
}