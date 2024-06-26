using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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


        public string GetContrasenaAPartirDeUsuario(string username)
        {
           
            var password = _connection.QueryFirstOrDefault<string>(
                @"SELECT ""Contrasenia"" FROM ""public"".""Login"" 
                           WHERE ""Usuario"" = @Username;",
            
                new { Username = username }
            );
            _connection.Close();
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
            _connection.Close();
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
            _connection.Close();
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
            }); _connection.Close();
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
            }); _connection.Close();
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

            _connection.Close();
            return usuario;
            
        }


        public Usuario BusqueUsuarioPorCedula(string cedula)
        {
            var usuario = _connection.QueryFirstOrDefault<Usuario>(
                 @"SELECT * FROM ""Usuario"" WHERE ""Cedula"" = @Cedula;",
                new { Cedula = cedula }
            );
            _connection.Close();

            return usuario;
        }

        public List<Canton> BuscarCantonesPorIdProvincia(int idProvincia)
        {
            var cantones = _connection.Query<Canton>(
                @"SELECT * FROM ""Canton"" WHERE ""IdProvincia"" = @IdProvincia;",
                new { IdProvincia = idProvincia }
            ).ToList();
            _connection.Close();

            return cantones;
        }

       


             public List<Distrito> BuscarDistritosPorIdCanton(int IdCanton)
        {
            var Distritos = _connection.Query<Distrito>(
                @"SELECT * FROM ""Distrito"" WHERE ""IdCanton"" = @IdCan;",
                new { IdCan = IdCanton }
            ).ToList();
            _connection.Close();

            return Distritos;
        }

        public List<Barrio> BuscarBarriosPorIdDistrito(int idDistrito)
        {
            var barrios = _connection.Query<Barrio>(
                @"SELECT * FROM ""Barrio"" WHERE ""IdDistrito"" = @IdDistrito;",
                new { IdDistrito = idDistrito }
            ).ToList();
            _connection.Close();
            return barrios;
        }

        public void InsertarDireccionUsuario(string cedulaUsuario, int idProvincia, int idCanton, int idDistrito, int idBarrio, string calle, string numero, string piso)
        {
            try
            {
                var sql = @"
CALL public.insertar_direccion_usuario(@CedulaUsuario, @IdProvincia, @IdCanton, @IdDistrito, @IdBarrio, @Calle, @Numero, @Piso);";

                _connection.Execute(sql, new
                {
                    CedulaUsuario = cedulaUsuario,
                    IdProvincia = idProvincia,
                    IdCanton = idCanton,
                    IdDistrito = idDistrito,
                    IdBarrio = idBarrio,
                    Calle = calle,
                    Numero = numero,
                    Piso = piso
                });
                _connection.Close();
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar dirección usuario: {ex.Message}");
                throw; // Lanza la excepción para manejarla en un nivel superior
            }
        }





        public bool ExisteDireccionParaUsuario(string cedulaUsuario)
        {
            var sql = @"
    SELECT EXISTS (
        SELECT 1
        FROM public.""DireccionUsuario""
        WHERE ""CedulaUsuario"" = @CedulaUsuario
    );";

            var existe = _connection.ExecuteScalar<bool>(sql, new
            {
                CedulaUsuario = cedulaUsuario
            });
            _connection.Close();

            return existe;
        }






    }
}
