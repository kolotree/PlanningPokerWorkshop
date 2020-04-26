using System.Threading.Tasks;
using Domain;

namespace Ports
{
    public interface IStore
    {
        public Task SaveChanges<T>(T aggregate) where T : AggregateRoot;
    }
}