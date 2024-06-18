using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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

        public List<Restaurante> BusqueRestaurantesEnUnaZona(int idDireccion)
        {
            int idBarrio = ObtengaIdBarrioDeDireccion(idDireccion);
            List<Restaurante> restaurantes = _connection.Query<Restaurante>(
                @"SELECT r.""Id"", r.""IdDireccion"", r.""Nombre"", r.""Comision""
          FROM ""Coberturas"" c
          JOIN ""Restaurante"" r ON c.""IdRestaurante"" = r.""Id""
          WHERE c.""IdBarrio"" = @IdBarrio;",
                new { IdBarrio = idBarrio }
            ).ToList();
            return restaurantes;
        }

        public int ObtengaIdBarrioDeDireccion(int idDireccion)
        {
            int idBarrio = _connection.QueryFirstOrDefault<int>(
                @"SELECT ""IdBarrio"" FROM ""Direccion"" WHERE ""Id"" = @IdDireccion;",
                new { IdDireccion = idDireccion }
            );

            return idBarrio;
        }
    }
}
