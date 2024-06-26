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
        public readonly IAdministradorDeCupon ElAdministradorDeCupones;
        public MenuPlatosController(IAdministradorDePlatos administrador, IAdministradorDeRestaurantes administradorDeRestaurantes, IAdministradorDeCupon administradorDeCupon)
        {
            ElAdministrador = administrador;
            ElAdministradorDeRestaurantes = administradorDeRestaurantes;
            ElAdministradorDeCupones = administradorDeCupon;
        }

        // GET: MenuPlatosController
        public ActionResult Index()
        {
            TempData["CodigoDelCupon"] = "estetelefonoparececarpinteroporquehacerin";
            List<PlatoVM> losPlatos = new List<PlatoVM>();
            List<Restaurante> restaurantesDisponiblesEnEsaDireccion;
            restaurantesDisponiblesEnEsaDireccion = ElAdministradorDeRestaurantes.ObtengaLaListaDeRestaurantes(User.Claims.FirstOrDefault(c => c.Type == "CedulaUsuario").Value.ToString());
            ViewBag.Restaurantes = restaurantesDisponiblesEnEsaDireccion;

            foreach (var restaurante in restaurantesDisponiblesEnEsaDireccion)
            {
                List<Plato> platos = new List<Plato>();
                platos = ElAdministrador.ObtengaLaListaDePlatosDeUnRestaurante(restaurante.Id);

                List<PlatoVM> platosVM = new List<PlatoVM>();
                foreach (var plato in platos)
                {
                    PlatoVM platoVM = new PlatoVM();
                    platoVM.Id = plato.Id;
                    platoVM.Nombre = plato.Nombre;
                    platoVM.Precio = plato.Precio;
                    platoVM.Descripcion = plato.Descripcion;
                    platoVM.NombreCategoria = plato.Categoria.Nombre;
                    platoVM.Image = plato.Image;
                    platosVM.Add(platoVM);
                }

                losPlatos.AddRange(platosVM);
            }


            return View(losPlatos);

        }

        [HttpPost]
        public IActionResult ProcesarCarrito([FromBody] List<ProductoCarrito> carrito)
        {
            // Almacenar el carrito en TempData
            TempData["Carrito"] = Newtonsoft.Json.JsonConvert.SerializeObject(carrito);
            return Json(new { success = true });


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

                return View("Error");
            }


            Pedido nuevoPedido = new Pedido
            {
                FechaDePedido = DateTime.Now,
                IdEstado = EnumEstadoPedido.REST,
                IdRestaurante = Convert.ToInt32(TempData["IdRest"]),
                CedulaCliente = User.Claims.FirstOrDefault(c => c.Type == "CedulaUsuario").Value.ToString(),
                ImporteTotal = carritoXD.Sum(item => item.Precio * item.Cantidad),
               

            };

            nuevoPedido.CodigoCupon = TempData["CodigoDelCupon"].ToString();
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
            Plato plato = ElAdministrador.ObtengaElPlato(id);




            PlatoVM platoVM = new PlatoVM();
            platoVM.Id = plato.Id;
            platoVM.Nombre = plato.Nombre;
            platoVM.Precio = plato.Precio;
            platoVM.Descripcion = plato.Descripcion;
            platoVM.IdRestaurante = plato.IdRestaurante;
            platoVM.Image = plato.Image;



            if (plato != null)
            {
                return Json(platoVM);
            }
            return NotFound();
        }


        public IActionResult ObtenerPlatosPorIdRestaurante(int idRestaurante)
        {
            List<Model.Plato> losPlatos;

            losPlatos = ElAdministrador.ObtengaLaListaDePlatosDeUnRestaurante(idRestaurante);

            List<PlatoVM> platosVM = new List<PlatoVM>();
            foreach (var plato in losPlatos)
            {
                PlatoVM platoVM = new PlatoVM();
                platoVM.Id = plato.Id;
                platoVM.Nombre = plato.Nombre;
                platoVM.Precio = plato.Precio;
                platoVM.Descripcion = plato.Descripcion;
                platoVM.NombreCategoria = plato.Categoria.Nombre;
                platoVM.Image = plato.Image;
                platosVM.Add(platoVM);
            }


            return Json(platosVM);
        }

        [HttpPost]
        public IActionResult VerificarCupon(string codigoCupon)
        {
          
            var cupon = ElAdministradorDeCupones.VerificarCupon(codigoCupon);
            if (cupon != null && cupon.FechaCaducidad >= DateTime.Now && cupon.UsosDisponibles > 0)
            {
                TempData["CodigoDelCupon"] = codigoCupon;
                // Aquí puedes aplicar el descuento o simplemente devolver información del cupón.
                return Json(new { success = true, mensaje = "Cupón válido.", descuento = cupon.PorcentajeDescuento });
            }
            else
            {
                return Json(new { success = false, mensaje = "Cupón inválido o expirado." });
            }
        }



    }
}
