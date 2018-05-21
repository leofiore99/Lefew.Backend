using Lefew.Domain.Models;
using Lefew.Domain.RepositoryInterfaces;
using Lefew.Repositories.Base;

namespace Lefew.Repositories.Repositories
{
    public class ProspectRepository : Repository<Prospect, Context>, IProspectRepository
    {

        public ProspectRepository(Context context) : base(context) { }
    }
}
