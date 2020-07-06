using System.Threading;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Microsoft.Extensions.Hosting;
using Webinar.EventStore;

namespace Webinar.Infrastructure
{
    public class EventStoreHostedService : IHostedService
    {
        readonly IEventStoreConnection _connection;
        readonly AllStreamSubscription _subscription;

        public EventStoreHostedService(IEventStoreConnection connection, AllStreamSubscription subscription)
        {
            _connection = connection;
            _subscription = subscription;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Connect to Event Store
            await _connection.ConnectAsync();
            
            // Start all subscriptions
            await _subscription.Subscribe(_connection);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _connection.Close();
            return Task.CompletedTask;
        }
    }
}