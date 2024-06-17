﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaCoMoLoCo_Express.BL;
using TaCoMoLoCo_Express.Model;

namespace TaCoMoLoCo_Express.UI.Controllers
{
    [Authorize]
    public class MenuPlatosController : Controller
    {
        public readonly IAdministradorDePlatos ElAdministrador;
        public readonly IAdministradorDeRestaurantes ElAdministradorDeRestaurantes;
        public MenuPlatosController(IAdministradorDePlatos administrador,IAdministradorDeRestaurantes administradorDeRestaurantes)
        {
            ElAdministrador = administrador;
            ElAdministradorDeRestaurantes = administradorDeRestaurantes;
        }

        // GET: MenuPlatosController
        public ActionResult Index(int idRestaurante)
        {
            List<Model.Plato> losPlatos;
            List<Restaurante> restaurantesDisponiblesEnEsaDireccion;
            restaurantesDisponiblesEnEsaDireccion = ElAdministradorDeRestaurantes.ObtengaLaListaDeRestaurantes(User.Claims.FirstOrDefault(c => c.Type == "CedulaUsuario").Value.ToString());
            ViewBag.Restaurantes = restaurantesDisponiblesEnEsaDireccion;
            losPlatos = ElAdministrador.ObtengaLaListaDePlatos(idRestaurante);
            return View(losPlatos);

        }

       /* [HttpPost]
        public ActionResult AgregarAlCarrito(int platoId)
        {
            var plato = ElAdministrador.ObtengaElPlato(platoId);
            if (plato != null)
            {
                // Recuperar el carrito de la sesión o crear uno nuevo si no existe
                var carrito = HttpContext.Session.Get<List<Plato>>("Carrito") ?? new List<Plato>();
                carrito.Add(plato);
                HttpContext.Session.Set("Carrito", carrito);
            }
            // Redirige a la acción Index del controlador actual después de agregar al carrito
            return RedirectToAction(nameof(Index));
        }
       */
        // GET: MenuPlatosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MenuPlatosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuPlatosController/Create
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

        // GET: MenuPlatosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenuPlatosController/Edit/5
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

        // GET: MenuPlatosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuPlatosController/Delete/5
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
