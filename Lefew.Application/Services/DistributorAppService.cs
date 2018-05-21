using Lefew.Application.InputModels;
using Lefew.Application.Interfaces;
using Lefew.Domain.Models;
using Lefew.Domain.RepositoryInterfaces;
using Lefew.Shared.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lefew.Application.Services
{
    public class DistributorAppService : IDistributorAppService
    {
        private IDistributorRepository _repository;
        private IProspectRepository _prospectRepository;

        public DistributorAppService(IDistributorRepository repository, IProspectRepository prospectRepository)
        {
            _repository = repository;
            _prospectRepository = prospectRepository;
        }

        public async Task<Distributor> Add(Distributor input)
        {
            var distributor = new Distributor(input.Email, input.Tries);

            await _repository.Insert(distributor);
            return distributor;
        }

        public async Task<Distributor> Get(int id)
        {
            var distributor = await _repository.GetById(id);

            if (distributor == null)
                throw new NotFoundException("Distributor não encontrado", id);

            return distributor;
        }

        public async Task<Distributor> GetByEmail(string email)
        {
            var distributor = await _repository.GetByEmail(email);

            //if (distributor == null)
            //    throw new NotFoundException("Distributor não encontrado", 0);

            return distributor;
        }

        public async Task<List<Distributor>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Remove(int id)
        {
            var distributor = await Get(id);
            if (distributor == null)
                throw new NotFoundException("Distributor não encontrado", id);

            await _repository.Delete(distributor);
        }

        public async Task Update(int id, Distributor input)
        {
            var distributor = await Get(id);

            if (distributor == null)
                throw new NotFoundException("Distributor não encontrado", id);

            distributor.UpdateInfo(input.Email, input.Tries);
            await _repository.Update(distributor);
        }

        public async Task<Distributor> Process(ProcessInputModel input)
        {
            var distributor = await GetByEmail(input.DistributorEmail);

            if (distributor == null)
            {
                distributor = new Distributor(input.DistributorEmail, 0);

                await _repository.Insert(distributor);
            }
            else
            {
                distributor.AddTry();
                await _repository.Update(distributor);
            }

            await _prospectRepository.Insert(new Prospect(input.ProspectEmail, distributor));

            return distributor;
        }
    }
}
