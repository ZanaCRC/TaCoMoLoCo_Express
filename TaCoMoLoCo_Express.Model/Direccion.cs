using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaCoMoLoCo_Express.Model
{
    public class Direccion
    {
        public int Id { get; set; }

        [DisplayName("Identificador de la provincia")]
        [ForeignKey("Provincia")]
        public int IdProvincia { get; set; }
        public Provincia Provincia { get; set; }
        [DisplayName("Identificador del canton")]
        [ForeignKey("Canton")]
        public int IdCanton { get; set; }
        public Canton Canton { get; set; }
        [DisplayName("Identificador del distrito")]
        [ForeignKey("Distrito")]
        public int IdDistrito { get; set; }
        public Distrito Distrito { get; set; }
        [ForeignKey("Barrio")]
        [DisplayName("Identificador del barrio")]
        public int IdBarrio { get; set; }
        public Barrio Barrio { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }

        public string Piso { get; set; }
        
    }
}
