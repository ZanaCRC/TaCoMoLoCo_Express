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

        public List<Categoria> ObtengaLasCategorias()
        {
            return _contexto.Categoria.ToList();
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
                if (pedido.CodigoCupon == "estetelefonoparececarpinteroporquehacerin")
                {
                    command.Parameters.Add(new NpgsqlParameter("@CodigoCupon", NpgsqlDbType.Varchar) { Value = "0" });


                }
                else {
                    command.Parameters.Add(new NpgsqlParameter("@CodigoCupon", NpgsqlDbType.Varchar) { Value = pedido.CodigoCupon});

                }
                

                command.Parameters.Add(new NpgsqlParameter("@FechaPedido", NpgsqlDbType.Date) { Value = pedido.FechaPedido.Date });
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
                _contexto.Database.CloseConnection();
            }
            
        }

        public void CrearDetallePedido(DetallePedido dp)
        {
            using (var command = _contexto.Database.GetDbConnection().CreateCommand())
            {
                // Definir el comando para llamar al procedimiento almacenado
                command.CommandText = "CALL public.insertar_detalle_pedido(@CodigoPedido, @IdPlato, @Unidades, @Precio);";

                // Agregar los parámetros necesarios para el procedimiento almacenado
                command.Parameters.Add(new NpgsqlParameter("@CodigoPedido", NpgsqlDbType.Integer) { Value = dp.CodigoPedido });
                command.Parameters.Add(new NpgsqlParameter("@IdPlato", NpgsqlDbType.Integer) { Value = dp.IdPlato });
                command.Parameters.Add(new NpgsqlParameter("@Unidades", NpgsqlDbType.Integer) { Value = dp.Unidades });
                command.Parameters.Add(new NpgsqlParameter("@Precio", NpgsqlDbType.Numeric) { Value = dp.Precio });

                // Abrir la conexión si está cerrada
                if (command.Connection.State != System.Data.ConnectionState.Open)
                {
                    _contexto.Database.OpenConnection();
                }

                // Ejecutar el comando
                command.ExecuteNonQuery();
                _contexto.Database.CloseConnection();
            }
        }

    }
}
