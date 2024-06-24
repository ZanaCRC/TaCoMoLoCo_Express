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
            return Json(new { success = true});
            
         
        }


        public IActionResult ConfirmacionCompra()
        {
            // Leer el carrito desde TempData
            var carritoJson = TempData["Carrito"] as string;
            List<ProductoCarrito> carrito = null;
            if (!string.IsNullOrEmpty(carritoJson))
            {
                carrito = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductoCarrito>>(carritoJson);
                TempData["Carrito"] = carritoJson;
            }

            TempData["IdRest"] = carrito[0].IdRestaurante;
           
            return View(carrito);
        }

        
        public IActionResult RealizarPago()
        {
            var carritoJson = TempData["Carrito"] as string;
            List<ProductoCarrito> carritoXD = null;
            if (!string.IsNullOrEmpty(carritoJson))
            {
                carritoXD = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductoCarrito>>(carritoJson);
                TempData["Carrito"] = carritoJson;
            }

            if (carritoXD == null || !carritoXD.Any())
            {
                // Manejar el caso en que el carrito esté vacío
                return View("Error"); // Asegúrate de tener una vista de Error o maneja este caso como prefieras.
            }

            // Aquí iría la lógica para crear el objeto Pedido y los objetos DetallePedido
            // basada en la lista de ProductoCarrito recibida
            Pedido nuevoPedido = new Pedido
            {
                FechaDePedido = DateTime.Now,
                IdEstado = EnumEstadoPedido.REST,
                IdRestaurante = Convert.ToInt32(TempData["IdRest"]),
                CedulaCliente = User.Claims.FirstOrDefault(c => c.Type == "CedulaUsuario").Value.ToString(),
                ImporteTotal = carritoXD.Sum(item => item.Precio * item.Cantidad)



                 
                // Añade aquí más propiedades según sea necesario
            };

            var idPedido = ElAdministradorDeRestaurantes.CrearPedido(nuevoPedido);
            List<DetallePedido> detallesPedido = carritoXD.Select(item => new DetallePedido
            {
                IdPlato = item.Id,
                Unidades = item.Cantidad,
                Precio = item.Precio
            }).ToList();

            // Guardar el pedido y los detalles del pedido
            bool exito = GuardarPedido(nuevoPedido, detallesPedido);

            if (!exito)
            {
                // Manejar el caso de fallo al guardar el pedido
                return View("Error");
            }

            // Si todo fue exitoso, redirigir a una vista de confirmación del pedido
            return RedirectToAction("Index");
        }

        // Asegúrate de implementar este método según tu lógica de negocio
        private bool GuardarPedido(Pedido pedido, List<DetallePedido> detallesPedido)
        {
            // Lógica para guardar el pedido y los detalles en la base de datos
            return true;
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
