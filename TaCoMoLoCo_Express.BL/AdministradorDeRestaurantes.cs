using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaCoMoLoCo_Express.Model;

namespace TaCoMoLoCo_Express.BL
{
    public class AdministradorDeRestaurantes : IAdministradorDeRestaurantes
    {
        private readonly DA.DBContext _contexto;

        public AdministradorDeRestaurantes(DA.DBContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Restaurante>> ObtengaLaListaDeRestaurantes(string usuarioId)
        {
            // Define la consulta SQL explícita
            var consultaSQL = @"
        SELECT r.*
        FROM public.""Restaurante"" r
        JOIN public.""Coberturas"" c ON r.""Id"" = c.""IdRestaurante""
        JOIN public.""Direccion"" d ON c.""IdBarrio"" = d.""IdBarrio""
        JOIN public.""Usuario"" u ON d.""Id"" = u.""IdDireccion""
        WHERE u.""Cedula"" = {0}";

            // Ejecuta la consulta SQL utilizando FromSqlRaw y pasa el parámetro de cédula
            var restaurantes = await _contexto.Restaurantes
                .FromSqlRaw(consultaSQL, usuarioId)
                .ToListAsync();

            return restaurantes;
        }
    }
}
