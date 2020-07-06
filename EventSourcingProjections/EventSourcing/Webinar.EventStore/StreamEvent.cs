using Webinar.Library;

namespace Webinar.EventStore
{
    public class StreamEvent
    {
        public StreamEvent(object evt, Metadata meta)
        {
            Event    = evt;
            Metadata = meta;
        }

        public object Event { get; }

        public Metadata Metadata { get; }
    }
}
