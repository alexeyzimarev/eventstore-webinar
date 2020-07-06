using System.Threading.Tasks;

namespace Webinar.Library
{
    public interface IAggregateStore
    {
        Task Store<T>(T entity) where T : Aggregate;

        Task<T> Load<T>(string id) where T : Aggregate;
    }
}
