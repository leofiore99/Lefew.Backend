using Lefew.Application.InputModels;
using Lefew.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lefew.Application.Interfaces
{
    public interface IDistributorAppService
    {
        //queries
        Task<Distributor> Get(int id);

        Task<Distributor> GetByEmail(string email);

        Task<List<Distributor>> GetAll();

        // commands
        Task<Distributor> Add(Distributor input);

        Task Remove(int id);

        Task Update(int id, Distributor input);

        Task<Distributor> Process(ProcessInputModel input);
    }
}
