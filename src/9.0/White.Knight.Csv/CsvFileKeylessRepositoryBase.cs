using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using White.Knight.Definition;
using White.Knight.Interfaces;
using White.Knight.Interfaces.Command;
using White.Knight.Abstractions.Extensions;

namespace White.Knight.Csv
{
    public abstract class CsvFileKeylessRepositoryBase<TD>(
        CsvRepositoryOptions<TD> repositoryOptions) : IKeylessRepository<TD>
        where TD : new()
    {
        private readonly ICsvLoader<TD> _csvLoader = repositoryOptions.CsvLoader;
        private readonly IRepositoryExceptionWrapper _repositoryExceptionWrapper = repositoryOptions.ExceptionWrapper;

        public abstract Expression<Func<TD, object>> DefaultOrderBy();

        public async Task<RepositoryResult<TP>> QueryAsync<TP>(
            IQueryCommand<TD, TP> command,
            CancellationToken cancellationToken)
        {
            try
            {
                var queryable =
                    await
                        _csvLoader
                            .ReadAsync(cancellationToken);

                var results =
                    await
                        queryable
                            .PerformCommandQueryAsync(command);

                /*var spec =
                    command
                        .Specification;

                var projectionFunc =
                    (command
                         .ProjectionOptions?
                         .Projection ??
                     throw new UndefinedProjectionException()
                    )
                    .Compile();

                var results =
                    queryable
                        .Where(o => spec.IsSatisfiedBy(o))
                        .Select(o => projectionFunc.Invoke(o))
                        .ToArray();*/

                return results;
            }
            catch (Exception e)
            {
                throw RethrowRepositoryException(e);
            }
        }

        protected Exception RethrowRepositoryException(Exception exception)
        {
            return _repositoryExceptionWrapper != null
                ? _repositoryExceptionWrapper.Rethrow(exception)
                : exception;
        }
    }
}