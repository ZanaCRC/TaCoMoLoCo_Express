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
    public class AdministradorDePedidos : IAdministradorDePedidos
    {
        private NpgsqlConnection _connection;

        public AdministradorDePedidos(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public List<Pedido> BusqueListaDePedidosPorEnviar()
        {
            //EN LA BASE EL TIPO REST ES EL NUMERO 1
            var pedidos = _connection.Query<Pedido>(
                @"SELECT * FROM ""Pedido"" WHERE ""IdEstado"" = @estado;",
                new { estado = 1 }
            ).ToList();
            _connection.Close();
            return pedidos;
        }

        public List<Usuario> BusqueListaDeRepartidores()
        {
            var repartidores = _connection.Query<Usuario>(
                @"SELECT * FROM ""Usuario"" WHERE ""IdRol"" = @IdRol;",
                new { IdRol = 6 }
            ).ToList();
            _connection.Close();
            return repartidores;
        }

        public Usuario BusqueRepartidorPorCedula(string cedula)
        {
            var usuario = _connection.QueryFirstOrDefault<Usuario>(
                 @"SELECT * FROM ""Usuario"" WHERE ""Cedula"" = @Cedula;",
                new { Cedula = cedula }
            );
            _connection.Close();
            return usuario;
        }

        public Pedido BusquePedidoPorId(int codigo)
        {
            var pedido = _connection.QueryFirstOrDefault<Pedido>(
                @"SELECT * FROM ""Pedido"" WHERE ""Codigo"" = @Id;",
                new { Id = codigo }
            );
            _connection.Close();
            return pedido;
        }

        //Metodo implementado para la asignacion de un pedido a un repartidor
        public void ActualiceCedulaRepartidorYEstadoDelPedido(int pedidoId, string nuevaCedulaRepartidor)
        {
            var sql = @"UPDATE ""Pedido""
                SET ""CedulaRepartidor"" = @CedulaRepartidor,
                    ""IdEstado"" = @IdEstado
                WHERE ""Codigo"" = @Id";

            _connection.Execute(sql, new { CedulaRepartidor = nuevaCedulaRepartidor, IdEstado = 3, Id = pedidoId });
            _connection.Close();
        }

        public List<Pedido> BusqueListaDePedidosPorAceptar(string cedulaRepartidor)
        {
            var pedidos = _connection.Query<Pedido>(
                @"SELECT * FROM ""Pedido"" 
          WHERE ""CedulaRepartidor"" = @CedulaRepartidor 
          AND ""IdEstado"" = @Estado;",
                new { CedulaRepartidor = cedulaRepartidor, Estado = 3 }
            ).ToList();
            _connection.Close();
            return pedidos;
        }


        public void ActualiceEstadoDelPedido(int pedidoId, int nuevoEstado)
        {
            if (nuevoEstado == 4)
            {
                var sql = @"UPDATE ""Pedido""
            SET ""IdEstado"" = @IdEstado,
                ""FechaEntrega"" = @FechaEntrega
            WHERE ""Codigo"" = @Id";

                _connection.Execute(sql, new { IdEstado = nuevoEstado, FechaEntrega = DateTime.Now, Id = pedidoId });
            }
            else
            {
                var sql = @"UPDATE ""Pedido""
            SET ""IdEstado"" = @IdEstado
            WHERE ""Codigo"" = @Id";

                _connection.Execute(sql, new { IdEstado = nuevoEstado, Id = pedidoId });
            }

            _connection.Close();
        }



    }
}
