using System.Threading.Tasks;
using Domain;
using Ports;

namespace ApplicationServices
{
    public sealed class CreateSessionHandler
    {
        private readonly IStore _store;

        public CreateSessionHandler(IStore store)
        {
            _store = store;
        }
        
        public Task Execute(CreateSession c) => _store.SaveChanges(Session.NewOf(c.SessionId));
    }
}