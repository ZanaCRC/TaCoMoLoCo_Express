using Microsoft.EntityFrameworkCore;


namespace TaCoMoLoCo_Express.DA
    
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
    }
}
