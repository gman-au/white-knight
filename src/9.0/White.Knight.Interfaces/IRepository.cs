using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using White.Knight.Interfaces.Command;

namespace White.Knight.Interfaces
{
    public interface IRepository<T> : IKeylessRepository<T>
    {
        protected Expression<Func<T, object>> KeyExpression();

        Task<T> SingleRecordAsync(object key, CancellationToken cancellationToken = default);

        Task<T> SingleRecordAsync(ISingleRecordCommand<T> command, CancellationToken cancellationToken = default);

        Task<T> AddOrUpdateAsync(IUpdateCommand<T> command, CancellationToken cancellationToken = default);

        Task<object> DeleteRecordAsync(ISingleRecordCommand<T> command, CancellationToken cancellationToken = default);
    }
}