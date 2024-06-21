using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaCoMoLoCo_Express.Model
{
    public class DetallePedido
    {
        public int Id { get; set; }
        public int CodigoPedido { get; set; }
        public int IdPlato { get; set; }
        public int Unidades { get; set; }
        public double Precio { get; set; }
    }
}
