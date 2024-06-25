using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaCoMoLoCo_Express.DA;
using TaCoMoLoCo_Express.Model;

namespace TaCoMoLoCo_Express.BL
{
    public class AdministradorDePlatos : IAdministradorDePlatos
    {
        private NpgsqlConnection ElContexto;
        public AdministradorDePlatos(NpgsqlConnection contexto)
        {
            ElContexto = contexto;
        }

        public List<Plato> ObtengaLaListaDePlatosDeUnRestaurante(int idRestaurante)
        {
            var query = @"
SELECT p.""Id"", p.""Nombre"", p.""Descripcion"", p.""Precio"", p.""IdCategoria"", p.""IdRestaurante"",
       c.""Id"" AS ""CategoriaId"", c.""Nombre"" AS ""NombreCategoria"",
       p.""Image"" AS ""Foto""
FROM ""Plato"" p
INNER JOIN ""Categoria"" c ON p.""IdCategoria"" = c.""Id""
WHERE p.""IdRestaurante"" = @IdRestaurante";

            var resultado = ElContexto.Query(query, new { IdRestaurante = idRestaurante }).ToList();
            var platosDelRestaurante = new List<Plato>();

            foreach (var fila in resultado)
            {
                var categoria = new Categoria
                {
                    Id = fila.CategoriaId,
                    Nombre = fila.NombreCategoria
                };

                var plato = new Plato
                {
                    Id = fila.Id,
                    Nombre = fila.Nombre,
                    Descripcion = fila.Descripcion,
                    Precio = fila.Precio,
                    IdCategoria = fila.IdCategoria,
                    IdRestaurante = fila.IdRestaurante,
                    Categoria = categoria,
                    Image = fila.Foto  // Asignar directamente el array de bytes
                };

                platosDelRestaurante.Add(plato);
            }

            return platosDelRestaurante;
        }







        public Plato ObtengaElPlato(int id)
        {
            var plato = ElContexto.QueryFirstOrDefault<Plato>(
                @"SELECT * FROM ""Plato"" WHERE ""Id"" = @idPlato",
                new { idPlato = id }
            );

            return plato;
        }


    }
}
