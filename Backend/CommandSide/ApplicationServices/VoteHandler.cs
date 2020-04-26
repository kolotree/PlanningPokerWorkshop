using System.Threading.Tasks;
using Domain;
using Ports;

namespace ApplicationServices
{
    public sealed class VoteHandler
    {
        private readonly IStore _store;

        public VoteHandler(IStore store)
        {
            _store = store;
        }

        public async Task Execute(Vote c)
        {
            var session = await _store.Load<Session>(c.SessionId.ToString());
            session.Vote(c.Member, c.Points);
            await _store.SaveChanges(session);
        }
    }
}