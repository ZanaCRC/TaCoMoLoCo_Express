using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaCoMoLoCo_Express.DA;
using TaCoMoLoCo_Express.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaCoMoLoCo_Express.BL
{
    public class AdministradorDeUsuarios : IAdministradorDeUsuarios
    {

        public DBContext ElContexto;

        public AdministradorDeUsuarios(DBContext elContexto)
        {
            ElContexto = elContexto;
        }

        public async Task<bool> RegistrarUsuario(string new_cedula, string new_nombre1, string new_nombre2, string new_apellido1, string new_apellido2, string new_iddireccion)
        {
            try
            {
                // Utiliza parámetros en la consulta SQL para prevenir ataques de inyección
                int registrosAfectados = await ElContexto.Database
                    .ExecuteSqlRawAsync("EXEC RegistrarUsuario @Cedula, @Nombre1, @Nombre2, @Apellido1, @Apellido2, @IdDireccion",
                        new SqlParameter("@Cedula", new_cedula),
                        new SqlParameter("@Nombre1", new_nombre1),
                        new SqlParameter("@Nombre2", new_nombre2),
                        new SqlParameter("@Apellido1", new_apellido1),
                        new SqlParameter("@Apellido2", new_apellido2),
                        new SqlParameter("@IdDireccion", new_iddireccion));



                return registrosAfectados > 0; // Retorna true si al menos un registro fue afectado
            }
            catch (Exception ex)
            {
                // Maneja la excepción de manera significativa para tu aplicación, por ejemplo, registrándola
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
                return false; // Retorna false para indicar que hubo un error al registrar el usuario
            }

        }

    }
}
