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

        public List<Restaurante> ObtengaLaListaDeRestaurantes(string usuarioId)
        {
            
            var consultaSQL = @"
        SELECT r.*
        FROM public.""Restaurante"" r
        JOIN public.""Coberturas"" c ON r.""Id"" = c.""IdRestaurante""
        JOIN public.""Direccion"" d ON c.""IdBarrio"" = d.""IdBarrio""
        JOIN public.""Usuario"" u ON d.""Id"" = u.""IdDireccion""
        WHERE u.""Cedula"" = {0}";

           
            var restaurantes = _contexto.Restaurante
                .FromSqlRaw(consultaSQL, usuarioId)
                .ToList();

            return restaurantes;
        }
    }
}
