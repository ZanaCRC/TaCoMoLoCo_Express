using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaCoMoLoCo_Express.Model
{
    public class Barrio
    {
        
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdDistrito { get; set; }

        public ICollection<Direccion> Direcciones { get; set; }

    }
}
