using Microsoft.EntityFrameworkCore;
using TaCoMoLoCo_Express.Model;



namespace TaCoMoLoCo_Express.DA
    
{
    public class DBContext : DbContext
    {
        public DbSet<Model.Restaurante> Restaurante { get; set; }
       
        
        public DBContext(DbContextOptions<DBContext> options) : base(options)

        {

        }
        public DbSet<Cobertura> Coberturas { get; set; }
        // Definiciones de otros DbSets...

        public DbSet<Plato> Platos { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Login> Login { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("usuario");
            // Configura la clave primaria compuesta para Coberturas
            modelBuilder.Entity<Cobertura>()
                .HasKey(c => new { c.IdRestaurante, c.IdBarrio });
            // Configuraciones para tus otras entidades...
            base.OnModelCreating(modelBuilder);

       

        }

    }
}
