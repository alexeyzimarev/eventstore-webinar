using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace Webinar.EventStore
{
    public delegate Task HandleEvent(StreamEvent evt);

    public delegate Task<EventStoreCatchUpSubscription> Subscribe(IEventStoreConnection connection);
}
