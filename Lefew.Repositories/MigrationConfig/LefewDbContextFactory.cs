using Lefew.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Lefew.Repositories.MigrationConfig
{
    public class LefewDbContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = Lefew; Trusted_Connection = True;");
            return new Context(builder.Options);
        }
    }
}
