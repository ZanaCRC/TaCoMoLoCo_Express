using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaCoMoLoCo_Express.Model
{
    public class Pedido
    {
        [Key]
        public int Codigo { get; set; }
   
        public string CedulaCliente { get; set; }

        public string? CedulaRepartidor { get; set; }
        public Usuario Usuario { get; set; }

        
        public int IdRestaurante { get; set; }
        
        [ForeignKey("Cupon")]
        public string? CodigoCupon { get; set; }

        [ForeignKey("Estado")]
        public EnumEstadoPedido IdEstado { get; set; }
        public DateTime FechaDePedido { get; set; }
        public DateTime FechaDeEntrega { get; set; }
        public decimal ImporteTotal { get; set; }

    }
}
