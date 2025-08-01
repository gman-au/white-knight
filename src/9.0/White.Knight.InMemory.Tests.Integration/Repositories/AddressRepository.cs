using System;
using System.Linq.Expressions;
using White.Knight.InMemory.Attribute;
using White.Knight.InMemory.Options;
using White.Knight.Tests.Domain;

namespace White.Knight.InMemory.Tests.Integration.Repositories
{
    [IsInMemoryRepository]
    public class AddressRepository(IInMemoryRepositoryFeatures<Address> repositoryFeatures)
        : InMemoryKeylessRepositoryBase<Address>(repositoryFeatures)
    {
        public override Expression<Func<Address, object>> DefaultOrderBy()
        {
            return o => o.AddressId;
        }
    }
}