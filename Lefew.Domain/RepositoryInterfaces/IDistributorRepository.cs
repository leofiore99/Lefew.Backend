using Lefew.Domain.Models;
using Meritus.Shared.Repositories;
using System.Threading.Tasks;

namespace Lefew.Domain.RepositoryInterfaces
{
    public interface IDistributorRepository : IRepository<Distributor>
    {
        Task<Distributor> GetByEmail(string email);
    }
}
