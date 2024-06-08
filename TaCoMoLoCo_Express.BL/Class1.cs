using Dapper;
using Npgsql;

using System.Data;
using TaCoMoLoCo_Express.DA;
using TaCoMoLoCo_Express.Model;

namespace TaCoMoLoCo_Express.BL
{
    public class Class1
    {
        
            private NpgsqlConnection _connection;

            public Class1(NpgsqlConnection connection)
            {
                _connection = connection;
            }

        // EJEMPLO PARA SELECIONAR //
        public List<Restaurante> GetRestaurantesEnCoberturas()
        {
            var restaurantes = _connection.Query<Restaurante>("SELECT * FROM get_restaurantes_en_coberturas();").AsList();
            return restaurantes;
        }

        // EJEMPLO PARA MODIFICAR ALGO //
        public void InsertarRestaurante(string nombre)
        {
            _connection.Execute("CALL insertar_restaurante(@Nombre)", new { Nombre = nombre });
        }
    }
}
