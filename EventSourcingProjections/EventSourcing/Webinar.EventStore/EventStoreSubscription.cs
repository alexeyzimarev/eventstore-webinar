using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Webinar.EventStore.Logging;

namespace Webinar.EventStore
{
    public class AllStreamSubscription
    {
        static readonly ILog Logger = LogProvider.GetCurrentClassLogger();
        
        readonly string _name;
        readonly AllStreamCheckpointStore _checkpointStore;
        readonly List<HandleEvent> _handlers = new List<HandleEvent>();
        
        public AllStreamSubscription(string name, AllStreamCheckpointStore checkpointStore)
        {
            _name = name;
            _checkpointStore = checkpointStore;
        }

        public AllStreamSubscription AddHandler(HandleEvent handleEvent)
        {
            _handlers.Add(handleEvent);
            return this;
        }

        public async Task<EventStoreCatchUpSubscription> Subscribe(IEventStoreConnection connection)
        {
            var settings = new CatchUpSubscriptionSettings(10000, 500, false, true, _name);
            var position = await _checkpointStore.Load(_name);
            return connection.SubscribeToAllFrom(position, settings, EventAppeared);
            
            async Task EventAppeared(EventStoreCatchUpSubscription _, ResolvedEvent resolvedEvent)
            {
                if (resolvedEvent.IsSystemEvent()) return;

                try
                {
                    var streamEvent = resolvedEvent.Deserialize();
                    var subscription = (EventStoreAllCatchUpSubscription) _;
                    
                    await Task.WhenAll(_handlers.Select(x => x(streamEvent)));
                    
                    await _checkpointStore.Store(_name, subscription.LastProcessedPosition);
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Error occured while handling {@event}", resolvedEvent);
                }
            }
        }
    }
}
