using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Webinar.Library;

namespace Webinar.EventStore
{
    public static class ResolvedEventExtensions
    {
        public static StreamEvent Deserialize(this ResolvedEvent resolvedEvent)
        {
            var meta = JsonConvert.DeserializeObject<Metadata>(
                Encoding.UTF8.GetString(resolvedEvent.Event.Metadata));
            var dataType = TypeMapper.GetType(resolvedEvent.Event.EventType);
            var jsonData = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
            var data = JsonConvert.DeserializeObject(jsonData, dataType);
            return new StreamEvent(data, meta);
        }

        public static bool IsSystemEvent(this ResolvedEvent resolvedEvent) => resolvedEvent.Event.EventType.StartsWith("$");
    }
}