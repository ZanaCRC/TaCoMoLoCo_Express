using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaCoMoLoCo_Express.Model
{
    public class Cupon
    {
        [Key] 
        public int CodigoDescuento { get; set; }
        public DateTime FechaCaducidad { get; set; }

        public double PorcentajeDescuento { get; set; }
    }
}
