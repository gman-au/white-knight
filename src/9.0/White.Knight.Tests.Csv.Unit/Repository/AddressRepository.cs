using System;
using System.Linq.Expressions;
using White.Knight.Csv;
using White.Knight.Csv.Attribute;
using White.Knight.Tests.Domain;

namespace White.Knight.Tests.Csv.Unit.Repository
{
    [IsCsvRepository]
    public class AddressRepository(CsvRepositoryOptions<Address> repositoryOptions)
        : CsvFileKeylessRepositoryBase<Address>(repositoryOptions)
    {
        public override Expression<Func<Address, object>> DefaultOrderBy() => o => o.AddressId;
    }
}