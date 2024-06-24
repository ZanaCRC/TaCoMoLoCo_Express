using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;
using TaCoMoLoCo_Express.BL;
using TaCoMoLoCo_Express.Model;
using TaCoMoLoCo_Express.UI.ViewModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
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
        public ActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Login usuarioLogueo)
        {

            if (!ElAdministrador.ExisteElUsuario(usuarioLogueo.Usuario))
            {
                ViewData["Mensaje"] = "Credenciales incorrectas";
                return View();
            }
            if (usuarioLogueo.Contrasena != ElAdministrador.GetContrasenaAPartirDeUsuario(usuarioLogueo.Usuario))
            {
                ViewData["Mensaje"] = "Credenciales incorrectas";
                return View();
            }
            else
            {


                string cedulaDeQuienInicioSesion;
                cedulaDeQuienInicioSesion = ElAdministrador.BusqueUsuarioParaLogin(usuarioLogueo.Usuario).Cedula;
                Model.Usuario usuarioBuscado = ElAdministrador.BusqueUsuarioPorCedula(cedulaDeQuienInicioSesion);
                TempData["CedulaUsuario"] = cedulaDeQuienInicioSesion;
                List<Claim> claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, usuarioBuscado.Nombre1 + usuarioBuscado.Nombre2 + usuarioBuscado.Apellido1 + usuarioBuscado.Apellido2),
                            new Claim(ClaimTypes.Role, usuarioBuscado.IdRol.ToString()),
                            new Claim(ClaimTypes.StreetAddress, usuarioBuscado.IdDireccion.ToString()),
                            new Claim("CedulaUsuario", usuarioBuscado.Cedula)


                        };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties prop = new AuthenticationProperties
                {
                    IsPersistent = true,
                    //ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    AllowRefresh = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), prop);

                var roleClaim = claimsIdentity.FindFirst(claimsIdentity.RoleClaimType);

                if (roleClaim.Value == "Cliente")
                {


                    //string CedulaDelClinete = ObtenerCedulaDelClienteAutenticado();

                   //Model.Usuario usuarioRegistrado =  ElAdministrador.BusqueUsuarioPorCedula(CedulaDelClinete);

                    if (ElAdministrador.ExisteDireccionParaUsuario(cedulaDeQuienInicioSesion)) { 
                        return RedirectToAction("Index", "MenuPlatos"); 
                    }
                    else {
                        return RedirectToAction("RegistrarDireccion", "Usuarios");
                        }


                  

                }


                else if (usuarioBuscado.IdRol == Model.EnumRol.Distribuidor)
                {
                    /*return RedirectToAction("Index", "Cliente");*/
                    ViewData["Mensaje"] = "Inicio de sesion correcto";

                }

                else if (usuarioBuscado.IdRol == Model.EnumRol.Repartidor)
                {
                    /*return RedirectToAction("Index", "Cliente");*/
                    ViewData["Mensaje"] = "Inicio de sesion correcto";

                }


            }
            return View();


        }


        // GET: UsuariosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuariosController/Create
        public ActionResult RegistrarDireccion()
        {
            return View();
        }
      

       
        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarDireccion(Model.Direccion direccionDelUsuario)
        {
            try
            {
                string cedulaDeQuienInicioSesion = TempData["CedulaUsuario"].ToString();

                ElAdministrador.InsertarDireccionUsuario(cedulaDeQuienInicioSesion, direccionDelUsuario.IdProvincia, direccionDelUsuario.IdCanton, direccionDelUsuario.IdDistrito, direccionDelUsuario.IdBarrio, direccionDelUsuario.Calle, direccionDelUsuario.Numero, direccionDelUsuario.Piso);
                return RedirectToAction("Index", "MenuPlatos");

               
            }
            catch
            {
                return View();
            }
        }
        public string ObtenerCedulaDelClienteAutenticado()
        {
            var cedulaClaim = User.FindFirst("CedulaUsuario");
            return cedulaClaim?.Value;
        }

        [HttpGet]
        public IActionResult ObtenerCantones(int provinciaId)
        {
            
            var cantones = ElAdministrador.BuscarCantonesPorIdProvincia(provinciaId);

            
            return Json(cantones);
        }

        [HttpGet]
        public IActionResult ObtenerDistritos(int cantonId)
        {
            var distritos = ElAdministrador.BuscarDistritosPorIdCanton(cantonId-1);   

            // Mapear los datos a objetos anónimos para evitar problemas de referencia circular
            var result = distritos.Select(d => new {
                id = d.Id,
                nombre = d.Nombre
            });

            return Json(result);
        }

        // Acción para obtener los barrios de un distrito
        [HttpGet]
        public IActionResult ObtenerBarrios(int distritoId)
        {
            var barrios = ElAdministrador.BuscarBarriosPorIdDistrito(distritoId);

            // Mapear los datos a objetos anónimos para evitar problemas de referencia circular
            var result = barrios.Select(b => new {
                id = b.Id,
                nombre = b.Nombre
            });

            return Json(result);
        }


        // GET: UsuariosController/Create
        public ActionResult RegistrarNuevoUsuario()
        {
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarNuevoUsuario(UsuarioLogin nuevoUsuario)
        {
            try
            {
                if (ElAdministrador.YaPoseeUnaCuenta(nuevoUsuario.informacion_usuario.Cedula))
                {
                    ViewData["Mensaje"] = "Ya existe un usuario registrado con esa cedula";
                    return View();
                }
                else
                {
                    if (!ElAdministrador.ExisteElUsuario(nuevoUsuario.informacion_login.Usuario))
                    {

                        int idRol = ElAdministrador.VerifiqueElRol(nuevoUsuario.informacion_usuario);

                        ElAdministrador.InsertarUsuario(nuevoUsuario.informacion_usuario.Cedula, nuevoUsuario.informacion_usuario.Nombre1, nuevoUsuario.informacion_usuario.Nombre2, nuevoUsuario.informacion_usuario.Apellido1, nuevoUsuario.informacion_usuario.Apellido2, idRol);
                        ElAdministrador.InsertarLogin(nuevoUsuario.informacion_usuario.Cedula, nuevoUsuario.informacion_login.Usuario, nuevoUsuario.informacion_login.Contrasena);

                        return RedirectToAction("Registrarse", "Usuarios");

                    }
                    else
                    {
                        ViewData["Mensaje"] = "Ya existe un usuario para login registrado con ese nombre de usuario";
                        return View();

                    }


                }


                ViewData["Mensaje"] = "No se pudo crear el usuario, error fatal";
                return View();

            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex;
                return View();
            }
        }

        /*
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
        }*/
    }
}
