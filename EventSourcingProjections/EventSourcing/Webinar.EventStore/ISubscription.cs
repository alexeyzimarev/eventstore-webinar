using EventStore.ClientAPI;

namespace Webinar.EventStore
{
    public interface ISubscription
    {
        void Subscribe(IEventStoreConnection connection);
    }
}
