using System.Threading.Tasks;
using Domain;

namespace Ports
{
    public interface IStore
    {
        Task<T> Load<T>(string id) where T : AggregateRoot, new();
        
        public Task SaveChanges<T>(T aggregate) where T : AggregateRoot;
    }
}