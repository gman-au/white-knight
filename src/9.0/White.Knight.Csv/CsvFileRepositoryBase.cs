using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using White.Knight.Abstractions.Extensions;
using White.Knight.Abstractions.Fluent;
using White.Knight.Interfaces;
using White.Knight.Interfaces.Command;

namespace White.Knight.Csv
{
    public abstract class CsvFileRepositoryBase<TD>(
        CsvRepositoryOptions<TD> repositoryOptions)
        : CsvFileKeylessRepositoryBase<TD>(repositoryOptions), IRepository<TD>
        where TD : new()
    {
        private readonly ICsvLoader<TD> _csvLoader = repositoryOptions.CsvLoader;

        public override Expression<Func<TD, object>> DefaultOrderBy()
        {
            return KeyExpression();
        }

        public abstract Expression<Func<TD, object>> KeyExpression();

        public virtual async Task<TD> SingleRecordAsync(object key, CancellationToken cancellationToken)
        {
            return await
                SingleRecordAsync(
                    key
                        .ToSingleRecordCommand<TD>(),
                    cancellationToken
                );
        }

        public async Task<TD> SingleRecordAsync(ISingleRecordCommand<TD> command, CancellationToken cancellationToken)
        {
            try
            {
                var key = command.Key;

                var selector =
                    key
                        .BuildKeySelectorExpression(KeyExpression());

                var csvEntity =
                    (await
                        _csvLoader
                            .ReadAsync(cancellationToken))
                    .FirstOrDefault(selector.Compile());

                return csvEntity;
            }
            catch (Exception e)
            {
                throw RethrowRepositoryException(e);
            }
        }

        public virtual async Task<TD> AddOrUpdateAsync(
            IUpdateCommand<TD> command,
            CancellationToken cancellationToken = default)
        {
            return await AddOrUpdateWithModifiedAsync(
                command.Entity,
                command.Inclusions,
                command.Exclusions,
                cancellationToken
            );
        }

        public async Task<object> DeleteRecordAsync(ISingleRecordCommand<TD> command,
            CancellationToken cancellationToken)
        {
            try
            {
                var key = command.Key;

                var selector =
                    key
                        .BuildKeySelectorExpression(KeyExpression());

                var allCsvEntities =
                    (await
                        _csvLoader
                            .ReadAsync(cancellationToken))
                    .Where(o => !selector.Compile().Invoke(o))
                    .ToList();

                await
                    _csvLoader
                        .WriteAsync(allCsvEntities, cancellationToken);

                return key;
            }
            catch (Exception e)
            {
                throw RethrowRepositoryException(e);
            }
        }

        private async Task<TD> AddOrUpdateWithModifiedAsync(
            TD sourceEntity,
            Expression<Func<TD, object>>[] fieldsToModify,
            Expression<Func<TD, object>>[] fieldsToPreserve,
            CancellationToken cancellationToken
        )
        {
            TD entityToCommit;

            try
            {
                var selector =
                    sourceEntity
                        .BuildEntitySelectorExpression(KeyExpression());

                var allCsvEntities =
                    (await
                        _csvLoader
                            .ReadAsync(cancellationToken))
                    .ToList();

                var filteredCsvEntities =
                    allCsvEntities
                        .Where(o => !selector.Compile().Invoke(o))
                        .ToList();

                var targetEntity =
                    allCsvEntities
                        .FirstOrDefault(o => selector.Compile().Invoke(o));

                entityToCommit =
                    sourceEntity
                        .ApplyInclusionStrategy(
                            targetEntity,
                            fieldsToModify,
                            fieldsToPreserve);

                // re-add to the final list
                filteredCsvEntities
                    .Add(entityToCommit);

                await
                    _csvLoader
                        .WriteAsync(filteredCsvEntities, cancellationToken);
            }
            catch (Exception e)
            {
                throw RethrowRepositoryException(e);
            }

            return entityToCommit;
        }
    }
}