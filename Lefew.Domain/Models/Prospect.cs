using Lefew.Shared.Entities;

namespace Lefew.Domain.Models
{
    public class Prospect : Entity
    {
        public string Email { get; private set; }

        public Distributor Distributor { get; private set; }

        private Prospect() { }

        public Prospect(string email, Distributor distributor)
        {
            Email = email;
            Distributor = distributor;
        }
    }
}
