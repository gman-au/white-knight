using System;
using System.Linq.Expressions;
using White.Knight.Csv;
using White.Knight.Csv.Attribute;
using White.Knight.Tests.Domain;

namespace White.Knight.Tests.Csv.Unit.Repository
{
    [IsCsvRepository]
    public class CustomerRepository(CsvRepositoryOptions<Customer> repositoryOptions)
        : CsvFileRepositoryBase<Customer>(repositoryOptions)
    {
        protected override Expression<Func<Customer, object>> Key() => b => b.CustomerId;
    }
}