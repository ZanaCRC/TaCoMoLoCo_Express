using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaCoMoLoCo_Express.Model;

namespace TaCoMoLoCo_Express.BL
{
    public class AdministradorDeCupones : IAdministradorDeCupon
    {
        private NpgsqlConnection _connection;
        public AdministradorDeCupones(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public Cupon VerificarCupon(string codigoCupon)
        {
            Cupon cupon = null;
            try
            {
                _connection.Open();
                string query = @"SELECT ""Codigo"", ""FechaCaducidad"", ""PorcentajeDescuento"", ""UsosDisponibles"" 
                         FROM ""Cupones"" 
                         WHERE ""Codigo"" = @codigoCupon";

                cupon = _connection.Query<Cupon>(query, new { codigoCupon }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Considera manejar la excepción de manera adecuada
                Console.WriteLine($"Ocurrió un error al verificar el cupón: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }

            return cupon;
        }

    }
}
