using Microsoft.EntityFrameworkCore;
using TaCoMoLoCo_Express.Model;



namespace TaCoMoLoCo_Express.DA
    
{
    public class DBContext : DbContext
    {
        public DbSet<Model.Restaurante> Restaurantes { get; set; }
       

        public DBContext(DbContextOptions<DBContext> options) : base(options)

        {

        }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Login> Login { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("usuario");
            // Configuraciones para tus otras entidades...

            base.OnModelCreating(modelBuilder);
        }

    }
}
