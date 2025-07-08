using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace White.Knight.Csv
{
    public interface ICsvLoader<T>
    {
        Task<IQueryable<T>> ReadAsync(CancellationToken cancellationToken);

        Task WriteAsync(IQueryable<T> records, CancellationToken cancellationToken);
    }
}