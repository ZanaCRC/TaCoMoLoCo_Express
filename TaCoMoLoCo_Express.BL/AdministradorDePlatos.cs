using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaCoMoLoCo_Express.DA;
using TaCoMoLoCo_Express.Model;

namespace TaCoMoLoCo_Express.BL
{
    public class AdministradorDePlatos : IAdministradorDePlatos
    {
        private NpgsqlConnection ElContexto;
        public AdministradorDePlatos(NpgsqlConnection contexto)
        {
            ElContexto = contexto;
        }

        public List<Plato> ObtengaLaListaDePlatosDeUnRestaurante(int idRestaurante)
        {

            var platosDelRestaurante = ElContexto.Query<Plato>(
                @"SELECT * FROM ""Plato"" WHERE ""IdRestaurante"" = @IdRestaurante",
                new { IdRestaurante = idRestaurante }
            ).ToList();

            return platosDelRestaurante;
        }

        public Plato ObtengaElPlato(int id)
        {
            var plato = ElContexto.QueryFirstOrDefault<Plato>(
                @"SELECT * FROM ""Plato"" WHERE ""Id"" = @idPlato",
                new { idPlato = id }
            );

            return plato;
        }


    }
}
