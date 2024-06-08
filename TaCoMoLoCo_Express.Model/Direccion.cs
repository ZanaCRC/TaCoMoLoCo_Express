using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaCoMoLoCo_Express.Model
{
    public class Direccion
    {
        public int Id { get; set; }

        [ForeignKey("Provincia")]
        public int IdProvincia { get; set; }
        public Provincia Provincia { get; set; }
        [ForeignKey("Canton")]
        public int IdCanton { get; set; }
        public Canton Canton { get; set; }

        [ForeignKey("Distrito")]
        public int IdDistrito { get; set; }
        public Distrito Distrito { get; set; }
        [ForeignKey("Barrio")]
        public int IdBarrio { get; set; }
        public Barrio Barrio { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }

        public string DireccionExacta { get; set; }
        
    }
}
