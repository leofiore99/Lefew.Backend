using Lefew.Shared.Entities;

namespace Lefew.Domain.Models
{
    public class Distributor : Entity
    {
        public string Email { get; private set; }

        public int Tries { get; private set; }

        public Distributor(string email, int tries)
        {
            Email = email;
            Tries = tries;
        }
    }
}
