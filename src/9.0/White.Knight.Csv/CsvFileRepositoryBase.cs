using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using White.Knight.Interfaces;
using White.Knight.Interfaces.Command;

namespace White.Knight.Csv
{
	public abstract class CsvFileRepositoryBase<TD>(CsvRepositoryOptions<TD> entityFrameworkRepositoryOptions)
		: CsvFileKeylessRepositoryBase<TD>(entityFrameworkRepositoryOptions), IRepository<TD>
		where TD : class
	{
		public override Expression<Func<TD, object>> DefaultOrderBy() => Key();

		protected abstract Expression<Func<TD, object>> Key();

		public Task<TD> SingleRecordAsync(object key, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<TD> SingleRecordAsync(ISingleRecordCommand<TD> command, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<TD> AddOrUpdateAsync(IUpdateCommand<TD> command, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<object> DeleteRecordAsync(ISingleRecordCommand<TD> command, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}