using System.Threading.Tasks;
using White.Knight.Abstractions.Fluent;
using White.Knight.Abstractions.Specifications;
using White.Knight.Interfaces;
using White.Knight.Tests.Domain;

namespace White.Knights.Tests.Integration.Context
{
    public partial class TestContextBase
    {
        private IRepository<Customer> _sut;

        public async Task ActSearchByAll()
        {
            _results =
                await
                    _sut
                        .QueryAsync
                        (
                            new SpecificationByAll<Customer>()
                                .ToQueryCommand()
                        );
        }
    }
}