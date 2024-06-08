using Dapper;
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

        private NpgsqlConnection _connection;

        public AdministradorDeUsuarios(NpgsqlConnection connection)
        {
            _connection = connection;
        }
        /*
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

        }*/

        public string GetContrasenaAPartirDeUsuario(string username)
        {
           
            var password = _connection.QueryFirstOrDefault<string>(
                @"SELECT ""Contrasenia"" FROM ""public"".""Login"" 
                           WHERE ""Usuario"" = @Username;",
            
                new { Username = username }
            );

            return password;
        }

        public bool YaPoseeUnaCuenta(string cedula)
        {
            
            var count = _connection.QueryFirstOrDefault<int>(
                @"SELECT COUNT(1)
                           FROM public.""Usuario""
                           WHERE ""Cedula"" = @Cedula;",
                new { Cedula = cedula }
            );

            return count > 0;
        }


        public bool ExisteElUsuario(string usuario)
        {

            var count = _connection.QueryFirstOrDefault<int>(
                @"SELECT COUNT(1)
                           FROM ""Login""
                           WHERE ""Usuario"" = @Usuario;",
                new { Usuario = usuario }
            );

            return count > 0;
        }

        public void InsertarUsuario(string cedula, string nombre1, string nombre2, string apellido1, string apellido2, int idRol)
        {
            var sql = @"
            CALL public.insertar_usuario (@Cedula, @Nombre1, @Nombre2, @Apellido1, @Apellido2, @IdRol);";

            _connection.Execute(sql, new
            {
                Cedula = cedula,
                Nombre1 = nombre1,
                Nombre2 = nombre2,
                Apellido1 = apellido1,
                Apellido2 = apellido2,
               
                IdRol = idRol
            });
        }

        public void InsertarLogin( string cedula, string usuario, string contrasenia)
        {
            var sql = @"
        CALL public.insertar_login (@Cedula, @Usuario, @Contrasenia);";

            _connection.Execute(sql, new
            {
                
                Cedula = cedula,
                Usuario = usuario,
                Contrasenia = contrasenia
            });
        }



        public int VerifiqueElRol(Usuario usuario) {

            int idRol;
            if (usuario.IdRol == Model.EnumRol.Cliente)
            {
                idRol = 1;
            }
            else if (usuario.IdRol == Model.EnumRol.Distribuidor)
            {
                idRol = 2;


            }
            else { idRol = 3; }


            return idRol;


        }


        public Login BusqueUsuarioParaLogin(string nombreUsuario)
        {
            var usuario = _connection.QueryFirstOrDefault<Login>(
                @"SELECT * FROM ""Login"" WHERE ""Usuario"" = @NombreUsuario;",
                new { NombreUsuario = nombreUsuario }
            );

            return usuario;
        }


        public Usuario BusqueUsuarioPorCedula(string cedula)
        {
            var usuario = _connection.QueryFirstOrDefault<Usuario>(
                 @"SELECT * FROM ""Usuario"" WHERE ""Cedula"" = @Cedula;",
                new { Cedula = cedula }
            );

            return usuario;
        }





    }
}
