using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaCoMoLoCo_Express.BL;

namespace TaCoMoLoCo_Express.UI.Controllers
{
    public class UsuariosController : Controller
    {
        public readonly IAdministradorDeUsuarios ElAdministrador;
        public UsuariosController(IAdministradorDeUsuarios elAdministrador)
        {

            ElAdministrador = elAdministrador;  

        }
        [HttpGet]
        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrarse(UsuarioVM usuario)
        {
            if (usuario.Clave != usuario.ConfirmarClave)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            bool agregadoCorrecto = ElAdministrador.RegistrarUsuario(usuario.Nombre, usuario.correoElectronico, usuario.Clave);
            if (agregadoCorrecto != false)
            {
                return RedirectToAction("InicieSesion", "Login");
            }

            ViewData["Mensaje"] = "No se pudo crear el usuario, error fatal";
            return View();


        }
        // GET: UsuariosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
