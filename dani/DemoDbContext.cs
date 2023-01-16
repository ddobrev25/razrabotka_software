using dani.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace dani
{
    public class DemoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options)
        {

        }
    }
}
