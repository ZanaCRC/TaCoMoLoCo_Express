using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaCoMoLoCo_Express.BL;
using TaCoMoLoCo_Express.Model;

namespace TaCoMoLoCo_Express.UI.Controllers
{
    public class RestaurantesController : Controller
    {
        public readonly IAdministradorDeRestaurantes ElAdministrador;

        public RestaurantesController(IAdministradorDeRestaurantes elAdministrador)
        {
            ElAdministrador = elAdministrador;
        }
        // GET: RestaurantesController
        public ActionResult Index()
        {
            string Cedula = User.Claims.FirstOrDefault(c => c.Type == "CedulaUsuario").Value.ToString();
            
            List<Restaurante> restaurantes = ElAdministrador.ObtengaLaListaDeRestaurantes(Cedula);
            return View(restaurantes);
        }

        // GET: RestaurantesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RestaurantesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RestaurantesController/Create
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

        // GET: RestaurantesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RestaurantesController/Edit/5
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

        // GET: RestaurantesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RestaurantesController/Delete/5
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
