using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaCoMoLoCo_Express.Model
{
    public class Cobertura
    {
        public int IdRestaurante { get; set; }
        public Restaurante Restaurante { get; set; }

        public int IdBarrio { get; set; }
        public Barrio Barrio { get; set; }
    }
}
