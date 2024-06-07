using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaCoMoLoCo_Express.Model
{
    public class Dia
    {
        public int Id { get; set; }
        public DayOfWeek ElDia { get; set; }
        public ICollection<Horario> Horarios { get; set; }
    }
}
