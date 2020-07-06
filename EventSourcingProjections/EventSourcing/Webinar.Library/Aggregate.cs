using System.Collections.Generic;
// ReSharper disable ParameterTypeCanBeEnumerable.Global

namespace Webinar.Library
{
    public abstract class Aggregate
    {
        protected void Apply(object evt)
        {
            _changes.Add(evt);
            When(evt);
        }
        
        public void Load(object[] events)
        {
            foreach (var @event in events)
            {
                When(@event);
                Version++;
            }
        }

        protected abstract void When(object evt);
        
        public IReadOnlyCollection<object> Changes => _changes.AsReadOnly();

        public void ClearChanges() => _changes.Clear();
        
        public abstract string GetId();
        
        public int Version { get; set; } = -1;
        
        List<object> _changes = new List<object>();
    }
}
