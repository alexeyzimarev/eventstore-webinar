using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace Webinar.EventStore
{
    public abstract class AllStreamCheckpointStore
    {
        public abstract Task<Position?> Load(string id);

        public abstract Task Store(string id, Position position);
    }
}
