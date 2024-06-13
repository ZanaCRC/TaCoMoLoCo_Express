using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaCoMoLoCo_Express.Model;
using System.Threading.Tasks;

namespace TaCoMoLoCo_Express.BL
{
    public interface IAdministradorDeRestaurantes
    {
       Task< List<Restaurante> >ObtengaLaListaDeRestaurantes(string usuarioId);

    }
}
