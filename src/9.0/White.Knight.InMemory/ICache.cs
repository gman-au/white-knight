using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace White.Knight.InMemory
{
    public interface ICache<T>
    {
        Task<IQueryable<T>> ReadAsync(CancellationToken cancellationTokens);

        Task WriteAsync(object key, T record, CancellationToken cancellationToken);

        Task RemoveAsync(object key, CancellationToken cancellationToken);
    }
}