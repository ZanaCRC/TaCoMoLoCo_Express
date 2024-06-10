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

            return pedidos;
        }

        public List<Usuario> BusqueListaDeRepartidores()
        {
            var repartidores = _connection.Query<Usuario>(
                @"SELECT * FROM ""Usuario"" WHERE ""IdRol"" = @IdRol;",
                new { IdRol = 6 }
            ).ToList();

            return repartidores;
        }

        public Usuario BusqueRepartidorPorCedula(string cedula)
        {
            var usuario = _connection.QueryFirstOrDefault<Usuario>(
                 @"SELECT * FROM ""Usuario"" WHERE ""Cedula"" = @Cedula;",
                new { Cedula = cedula }
            );

            return usuario;
        }

        public Pedido BusquePedidoPorId(int codigo)
        {
            var pedido = _connection.QueryFirstOrDefault<Pedido>(
                @"SELECT * FROM ""Pedido"" WHERE ""Codigo"" = @Id;",
                new { Id = codigo }
            );

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
        }




    }
}
