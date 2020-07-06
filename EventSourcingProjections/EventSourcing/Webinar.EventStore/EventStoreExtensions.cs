using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Webinar.Library;

namespace Webinar.EventStore
{
    public static class EventStoreExtensions
    {
        public static Task AppendEvents(this IEventStoreConnection connection,
            string streamName, long version,
            params StreamEvent[] events)
        {
            if (events == null || !events.Any()) return Task.CompletedTask;
            
            var preparedEvents = events
                .Select(evt =>
                    new EventData(
                        eventId: Guid.NewGuid(),
                        type: TypeMapper.GetName(evt.Event),
                        isJson: true,
                        data: Serialize(evt.Event),
                        metadata: Serialize(evt.Metadata)
                    ))
                .ToArray();
            return connection.AppendToStreamAsync(
                streamName,
                version,
                preparedEvents);
        }

        static byte[] Serialize(object data)
            => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
    }
}