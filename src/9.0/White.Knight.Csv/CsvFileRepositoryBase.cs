using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using White.Knight.Abstractions.Extensions;
using White.Knight.Interfaces;
using White.Knight.Interfaces.Command;

namespace White.Knight.Csv
{
    public abstract class CsvFileRepositoryBase<TD>(
        CsvRepositoryOptions<TD> repositoryOptions)
        : CsvFileKeylessRepositoryBase<TD>(repositoryOptions), IRepository<TD>
        where TD : class
    {
        private readonly ICsvLoader<TD> _csvLoader = repositoryOptions.CsvLoader;

        public override Expression<Func<TD, object>> DefaultOrderBy()
        {
            return KeyExpression();
        }

        public abstract Expression<Func<TD, object>> KeyExpression();

        public Task<TD> SingleRecordAsync(object key, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<TD> SingleRecordAsync(ISingleRecordCommand<TD> command, CancellationToken cancellationToken)
        {
            try
            {
                var key = command.Key;

                var selector =
                    key
                        .BuildKeySelectorExpression(KeyExpression());

                var record =
                    (await
                        _csvLoader
                            .ReadAsync(cancellationToken))
                    .FirstOrDefault(selector.Compile());

                return record;
            }
            catch (Exception e)
            {
                throw RethrowRepositoryException(e);
            }
        }

        public async Task<TD> AddOrUpdateAsync(
            IUpdateCommand<TD> command,
            CancellationToken cancellationToken)
        {
            try
            {
                var selector =
                    command
                        .Entity
                        .BuildKeySelectorExpression(KeyExpression());

                var csvEntity =
                    (await
                        _csvLoader
                            .ReadAsync(cancellationToken))
                    .FirstOrDefault(selector.Compile());

                if (csvEntity != null)
                {
                    return csvEntity;
                }
            }
            catch (Exception e)
            {
                throw RethrowRepositoryException(e);
            }

            return null;
        }

        public Task<object> DeleteRecordAsync(ISingleRecordCommand<TD> command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}