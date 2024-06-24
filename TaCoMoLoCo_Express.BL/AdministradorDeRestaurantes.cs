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
    }
}
