using Lefew.Shared.Entities;

namespace Lefew.Domain.Models
{
    public class Distributor : Entity
    {
        public string Email { get; private set; }

        public int Tries { get; private set; }

        private Distributor() { }

        public Distributor(string email, int tries)
        {
            Email = email;
            Tries = tries;
        }

        public void UpdateInfo(string email, int tries)
        {
            if (email != null)
                Email = email;

            Tries = tries;
        }

        public void AddTry()
        {
            Tries++;
        }
    }
}
