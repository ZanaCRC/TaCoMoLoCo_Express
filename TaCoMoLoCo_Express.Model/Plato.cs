using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaCoMoLoCo_Express.Model
{
    public class Plato
    {
        public int Id { get; set; }

        [ForeignKey("Restaurante")]
        public int IdRestaurante { get; set; }

        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }

        public Categoria Categoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
       //public byte[] Imagen { get; set; }
        
    }
}
