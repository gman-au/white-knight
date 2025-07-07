using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace White.Knight.Csv
{
    public interface ICsvLoader<T>
    {
        Task<IQueryable<T>> LoadAsync(CancellationToken cancellationToken);
    }
}