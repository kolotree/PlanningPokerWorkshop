using System.Threading.Tasks;
using Domain;
using Ports;

namespace ApplicationServices
{
    public sealed class ClearSessionHandler
    {
        private readonly IStore _store;

        public ClearSessionHandler(IStore store)
        {
            _store = store;
        }

        public async Task Execute(ClearSession c)
        {
            var session = await _store.Load<Session>(c.SessionId.ToString());
            session.Clear();
            await _store.SaveChanges(session);
        }
    }
}