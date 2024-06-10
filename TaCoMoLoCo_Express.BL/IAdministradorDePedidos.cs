using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaCoMoLoCo_Express.Model;

namespace TaCoMoLoCo_Express.BL
{
    public interface IAdministradorDePedidos
    {


        public List<Pedido> BusqueListaDePedidosPorEnviar();
        public List<Usuario> BusqueListaDeRepartidores();
        public Usuario BusqueRepartidorPorCedula(string cedula);
        public Pedido BusquePedidoPorId(int codigo);
        public void ActualiceCedulaRepartidorYEstadoDelPedido(int pedidoId, string nuevaCedulaRepartidor);
    }
}
