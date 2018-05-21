using Lefew.Domain.Models;
using Lefew.Domain.RepositoryInterfaces;
using Lefew.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lefew.Repositories.Repositories
{
    public class DistributorRepository : Repository<Distributor, Context>, IDistributorRepository
    {

        public DistributorRepository(Context context) : base(context) { }

        public virtual Task<Distributor> GetByEmail(string email)
        {
            return DbSet
                .FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
