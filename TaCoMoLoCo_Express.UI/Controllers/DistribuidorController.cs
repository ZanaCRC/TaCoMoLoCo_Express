using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaCoMoLoCo_Express.BL;
using TaCoMoLoCo_Express.Model;

namespace TaCoMoLoCo_Express.UI.Controllers
{
    public class DistribuidorController : Controller
    {

        public readonly IAdministradorDePedidos ElAdministradorDePedidos;

        public static int idPedidoAProcesar = 0;
        public DistribuidorController(IAdministradorDePedidos elAdministrador)
        {

            ElAdministradorDePedidos = elAdministrador;

        }

        // GET: DistribuidorController
        public ActionResult Index()
        {
            List<Pedido> ListaDePedidosPorEnviar = ElAdministradorDePedidos.BusqueListaDePedidosPorEnviar();


            return View(ListaDePedidosPorEnviar);
        }
      
        public ActionResult AsignacionDeRepartidor(int idPedidoAEnviar)
        {
            idPedidoAProcesar = idPedidoAEnviar;
            List<Usuario> ListaDeRepartidores = ElAdministradorDePedidos.BusqueListaDeRepartidores();



            return View(ListaDeRepartidores);
        }


        public ActionResult FinaliceLaAsignacion(string cedula)
            
        {

            int idAModificar = idPedidoAProcesar;
            ElAdministradorDePedidos.ActualiceCedulaRepartidorYEstadoDelPedido(idAModificar, cedula);

           

            idPedidoAProcesar = 0;


            return RedirectToAction("Index");
        }

        // GET: DistribuidorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DistribuidorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DistribuidorController/Create
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

        // GET: DistribuidorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DistribuidorController/Edit/5
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

        // GET: DistribuidorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DistribuidorController/Delete/5
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
