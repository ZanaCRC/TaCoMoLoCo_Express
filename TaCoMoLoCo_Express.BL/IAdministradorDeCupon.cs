using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaCoMoLoCo_Express.Model;

namespace TaCoMoLoCo_Express.BL
{
    public interface IAdministradorDeCupon
    {

        Cupon VerificarCupon(string codigoCupon);

    }
}
