using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaCoMoLoCo_Express.Model
{
    public class Horario
    {
        public int Id { get; set; }
        [ForeignKey("Restaurante")]
        public int IdRestaurante { get; set; }
        public Restaurante Restaurante { get; set; }
        [ForeignKey("Dia")]
        public int IdDia { get; set; }
        public Dia Dia { get; set; }
    }
}
