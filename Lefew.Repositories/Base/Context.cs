using Lefew.Domain.Models;
using Lefew.Repositories.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Lefew.Repositories.Base
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Prospect> Badges { get; set; }
        public DbSet<Distributor> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeamentos
            modelBuilder.ApplyLefewTableMappings();
        }
    }
}
