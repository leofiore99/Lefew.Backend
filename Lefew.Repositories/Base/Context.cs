using Lefew.Domain.Models;
using Lefew.Repositories.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Lefew.Repositories.Base
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Prospect> Prospects { get; set; }
        public DbSet<Distributor> Distributors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeamentos
            modelBuilder.ApplyLefewTableMappings();
        }
    }
}
