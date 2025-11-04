using System;
using System.Linq.Expressions;
using White.Knight.InMemory.Attribute;
using White.Knight.InMemory.Options;
using White.Knight.Tests.Domain;

namespace White.Knight.InMemory.Tests.Integration.Repositories
{
    [IsInMemoryRepository]
    public class CustomerRepository(IInMemoryRepositoryFeatures<Customer> repositoryFeatures)
        : InMemoryRepositoryBase<Customer>(repositoryFeatures)
    {
        public override Expression<Func<Customer, object>> KeyExpression()
        {
            return b => b.CustomerId;
        }
    }
}