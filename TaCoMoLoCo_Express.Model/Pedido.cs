using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaCoMoLoCo_Express.Model
{
    public class Pedido
    {
        public int Codigo { get; set; }
        [ForeignKey("Usuario")]
        public int CedulaCliente { get; set; }

        [ForeignKey("Usuario")]
        public int? IdRepartidor { get; set; }
        public Usuario Usuario { get; set; }

        [ForeignKey("Restaurante")]
        public int IdRestaurante { get; set; }
        public Restaurante Restaurante { get; set; }

        [ForeignKey("Cupon")]
        public int? CodigoCupon { get; set; }

        [ForeignKey("Estado")]
        public int IdEstado { get; set; }
        public EnumEstadoPedido Estado { get; set; }
        public DateTime FechaDePedido { get; set; }
        public DateTime FechaDeEntrega { get; set; }
        public double ImporteTotal { get; set; }

    }
}
