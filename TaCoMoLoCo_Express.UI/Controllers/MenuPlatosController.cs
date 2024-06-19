using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaCoMoLoCo_Express.BL;
using TaCoMoLoCo_Express.Model;
using TaCoMoLoCo_Express.UI.ViewModel;

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
        public ActionResult Index()
        {
            List<Model.Plato> losPlatos = new List<Plato>();
            List<Restaurante> restaurantesDisponiblesEnEsaDireccion;
            restaurantesDisponiblesEnEsaDireccion = ElAdministradorDeRestaurantes.ObtengaLaListaDeRestaurantes(User.Claims.FirstOrDefault(c => c.Type == "CedulaUsuario").Value.ToString());
            ViewBag.Restaurantes = restaurantesDisponiblesEnEsaDireccion;

            foreach (var restaurante in restaurantesDisponiblesEnEsaDireccion)
            {
                losPlatos.AddRange(ElAdministrador.ObtengaLaListaDePlatosDeUnRestaurante(restaurante.Id));
            }


            return View(losPlatos);

        }

        [HttpPost]
        public IActionResult ProcesarCarrito([FromBody] List<ProductoCarrito> carrito)
        {
            // Almacenar el carrito en TempData
            TempData["Carrito"] = Newtonsoft.Json.JsonConvert.SerializeObject(carrito);

            // Redireccionar a la acción de confirmación de compra
            return RedirectToAction("ConfirmacionCompra");
        }


        public IActionResult ConfirmacionCompra()
        {
            // Leer el carrito desde TempData
            var carritoJson = TempData["Carrito"] as string;
            List<ProductoCarrito> carrito = null;
            if (!string.IsNullOrEmpty(carritoJson))
            {
                carrito = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductoCarrito>>(carritoJson);
            }

            // Pasar el carrito a la vista
            return View(carrito);
        }

        public IActionResult ObtenerPlato(int id)
        {
            var plato = ElAdministrador.ObtengaElPlato(id);

            
            if (plato != null)
            {
                return Json(plato);
            }
            return NotFound();
        }


        public IActionResult ObtenerPlatosPorIdRestaurante(int idRestaurante)
        {
            List<Model.Plato> losPlatos;

            losPlatos = ElAdministrador.ObtengaLaListaDePlatosDeUnRestaurante(idRestaurante);

            return Json(losPlatos);
        }

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
