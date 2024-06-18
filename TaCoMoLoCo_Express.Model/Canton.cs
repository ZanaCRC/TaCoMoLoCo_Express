using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaCoMoLoCo_Express.Model
{
    public class Canton
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdProvincia { get; set; }
        public ICollection<Direccion> Direcciones { get; set; }
    }
}
