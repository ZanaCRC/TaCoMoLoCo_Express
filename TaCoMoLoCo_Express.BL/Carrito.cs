using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaCoMoLoCo_Express.Model;

namespace TaCoMoLoCo_Express.BL
{
    public class Carrito : Icarrito
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public Carrito(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AgregarAlCarrito(Plato plato)
        {
            var carrito = _session.GetObjectFromJson<List<Plato>>("Carrito") ?? new List<Plato>();
            carrito.Add(plato);
            _session.SetObjectAsJson("Carrito", carrito);
        }

        public List<Plato> ObtenerContenidoDelCarrito()
        {
            return _session.GetObjectFromJson<List<Plato>>("Carrito") ?? new List<Plato>();
        }

        public void EliminarDelCarrito(int platoId)
        {
            throw new NotImplementedException();
        }

        public void LimpiarCarrito()
        {
            throw new NotImplementedException();
        }

        // Otros métodos de la interfaz...
    }

}
