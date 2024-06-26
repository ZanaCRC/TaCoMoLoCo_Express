using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using TaCoMoLoCo_Express.Model;

namespace TaCoMoLoCo_Express.BL
{
    public class AdministradorDeRestaurantes : IAdministradorDeRestaurantes
    {
        private readonly DA.DBContext _contexto;
        private NpgsqlConnection _connection;
        public AdministradorDeRestaurantes(DA.DBContext contexto)
        {
            _contexto = contexto;
        }

        public List<Restaurante> ObtengaLaListaDeRestaurantes(string usuarioId)
        {

            var consultaSQL = @"
            SELECT r.*
            FROM public.""Restaurante"" r
            JOIN public.""Coberturas"" c ON r.""Id"" = c.""IdRestaurante""
            JOIN public.""DireccionUsuario"" du ON c.""IdBarrio"" = du.""IdBarrio""
            WHERE du.""CedulaUsuario"" = {0}
        ";


            var restaurantes = _contexto.Restaurante
                .FromSqlRaw(consultaSQL, usuarioId)
                .ToList();

            _connection.Close();


            return restaurantes;
        }


        public int ObtengaIdBarrioDeDireccion(int idDireccion)
        {
            int idBarrio = _connection.QueryFirstOrDefault<int>(
                @"SELECT ""IdBarrio"" FROM ""Direccion"" WHERE ""Id"" = @IdDireccion;",
                new { IdDireccion = idDireccion }
            );
            _connection.Close();

            return idBarrio;
        }

        public int CrearPedido(Pedido pedido)
        {
            using (var command = _contexto.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"
            SELECT * FROM public.crearpedidonuevo(
                @CedulaCliente::character varying, 
                @IdRestaurante::integer,
                @CodigoCupon::character varying,
                @FechaPedido::date,
                @IdEstado::integer,
                @ImporteTotal::numeric
            )";

                command.Parameters.Add(new NpgsqlParameter("@CedulaCliente", NpgsqlDbType.Varchar) { Value = pedido.CedulaCliente });
                command.Parameters.Add(new NpgsqlParameter("@IdRestaurante", NpgsqlDbType.Integer) { Value = pedido.IdRestaurante });
                command.Parameters.Add(new NpgsqlParameter("@CodigoCupon", NpgsqlDbType.Varchar) { Value = "2" });
                command.Parameters.Add(new NpgsqlParameter("@FechaPedido", NpgsqlDbType.Date) { Value = pedido.FechaDePedido.Date });
                command.Parameters.Add(new NpgsqlParameter("@IdEstado", NpgsqlDbType.Integer) { Value = 1 });
                command.Parameters.Add(new NpgsqlParameter("@ImporteTotal", NpgsqlDbType.Numeric) { Value = pedido.ImporteTotal });

                _contexto.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    if (result.Read())
                    {
                        return Convert.ToInt32(result[0]);
                    }
                    else
                    {
                        throw new Exception("Error creating order");
                    }
                }
                _connection.Close();
            }
            
        }
    }
}
