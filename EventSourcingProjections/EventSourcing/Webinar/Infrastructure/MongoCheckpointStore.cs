using System.Threading.Tasks;
using EventStore.ClientAPI;
using MongoDB.Driver;
using Webinar.EventStore;
using Webinar.Mongo;

namespace Webinar.Infrastructure
{
    public class MongoCheckpointStore { }

    public class MongoAllStreamCheckpointStore : AllStreamCheckpointStore
    {
        readonly IMongoCollection<Checkpoint> _collection;

        public MongoAllStreamCheckpointStore(IMongoDatabase database) => _collection = database.For<Checkpoint>();

        public override async Task<Position?> Load(string id)
        {
             var doc = await _collection.LoadDocument(id);
             return doc?.Position;
        }

        public override Task Store(string id, Position position) => _collection.ReplaceDocument(new Checkpoint {Id = id, Position = position});

        public class Checkpoint : Document
        {
            public Position Position { get; set; }
        }
    }
}
