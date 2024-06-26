using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaCoMoLoCo_Express.BL;
using TaCoMoLoCo_Express.Model;

namespace TaCoMoLoCo_Express.UI.Controllers
{
    public class RepartidorController : Controller
    {

        public readonly IAdministradorDePedidos ElAdministradorDePedidos;

      
        public RepartidorController(IAdministradorDePedidos elAdministrador)
        {

            ElAdministradorDePedidos = elAdministrador;

        }


        // GET: RepartidorController
        public ActionResult Index()
        {
            string Cedula = User.Claims.FirstOrDefault(c => c.Type == "CedulaUsuario").Value.ToString();
            List<Pedido> ListaDePedidosPorAceptar = ElAdministradorDePedidos.BusqueListaDePedidosPorAceptar(Cedula);


            return View(ListaDePedidosPorAceptar);
        }


        public ActionResult FinaliceElEnvio(int iDPedidoAFinalizar)
        {
            ElAdministradorDePedidos.ActualiceEstadoDelPedido(iDPedidoAFinalizar, 4);

            return RedirectToAction("Index");
        }


        public ActionResult RechazarPedido(int IDPedidoARechazar)
        {
            ElAdministradorDePedidos.ActualiceEstadoDelPedido(IDPedidoARechazar, 5);

            return RedirectToAction("Index");
        }


        // GET: RepartidorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RepartidorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RepartidorController/Create
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

        // GET: RepartidorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RepartidorController/Edit/5
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

        // GET: RepartidorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RepartidorController/Delete/5
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
