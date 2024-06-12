using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaCoMoLoCo_Express.Model;

namespace TaCoMoLoCo_Express.BL
{
    public interface Icarrito
    {

        void AgregarAlCarrito(Plato plato);
        void EliminarDelCarrito(int platoId);
        List<Plato> ObtenerContenidoDelCarrito();
        void LimpiarCarrito();

    }
}
