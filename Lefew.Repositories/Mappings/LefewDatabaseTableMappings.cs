using Lefew.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Lefew.Repositories.Mappings
{
    public static class LefewDatabaseTableMappings
    {
        public static ModelBuilder ApplyLefewTableMappings(this ModelBuilder modelBuilder)
        {
            return modelBuilder
               .Entity<Distributor>(entity =>
               {
                   entity
                   .ToTable("Distributor");

                   entity.Property(c => c.Email)
                   .HasColumnType("varchar(150)")
                   .IsRequired();
               })

               .Entity<Prospect>(entity =>
               {
                   entity
                   .ToTable("Prospect");

                   entity.Property(c => c.Email)
                   .HasColumnType("varchar(150)")
                   .IsRequired();
               })
               ;
        }
    }
}
