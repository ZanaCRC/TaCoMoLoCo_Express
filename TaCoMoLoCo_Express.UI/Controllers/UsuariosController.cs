﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using TaCoMoLoCo_Express.BL;
using TaCoMoLoCo_Express.Model;
using TaCoMoLoCo_Express.UI.ViewModel;

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
        public ActionResult Registrarse(Login usuarioLogueo)
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

                    if (ElAdministrador.BusqueUsuarioPorCedula(cedulaDeQuienInicioSesion).IdRol == Model.EnumRol.Cliente)
                    {
                        /*return RedirectToAction("Index", "Cliente");*/
                        ViewData["Mensaje"] = "Inicio de sesion correcto";

                    }


                    else if (ElAdministrador.BusqueUsuarioPorCedula(cedulaDeQuienInicioSesion).IdRol == Model.EnumRol.Distribuidor)
                    {
                        /*return RedirectToAction("Index", "Cliente");*/
                        ViewData["Mensaje"] = "Inicio de sesion correcto";

                    }

                    else if (ElAdministrador.BusqueUsuarioPorCedula(cedulaDeQuienInicioSesion).IdRol == Model.EnumRol.Repartidor)
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
