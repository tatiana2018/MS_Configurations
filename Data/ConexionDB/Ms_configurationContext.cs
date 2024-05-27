using Microsoft.EntityFrameworkCore;

namespace Data.ConexionDB
{
    public class Ms_configurationContext : DbContext
    {
        public Ms_configurationContext(DbContextOptions<Ms_configurationContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Image> Image { get; set; } = default!;


    }
}
