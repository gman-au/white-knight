using System.Threading;
using System.Threading.Tasks;
using White.Knight.Interfaces.Command;

namespace White.Knight.Interfaces
{
	public interface IRepository<T> : IKeylessRepository<T>
	{
		Task<T> SingleRecordAsync(object key, CancellationToken cancellationToken);

		Task<T> SingleRecordAsync(ISingleRecordCommand<T> command, CancellationToken cancellationToken);

		Task<T> AddOrUpdateAsync(IUpdateCommand<T> command, CancellationToken cancellationToken);

		Task<object> DeleteRecordAsync(ISingleRecordCommand<T> command, CancellationToken cancellationToken);
	}
}