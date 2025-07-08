using White.Knight.Definition;
using White.Knight.Tests.Domain;
using Xunit;

namespace White.Knights.Tests.Integration.Context
{
    public partial class TestContextBase
    {
        private RepositoryResult<Customer> _results;

        public void AssertRecordCountFour()
        {
            Assert.NotEmpty
                (_results?.Records ?? []);
            Assert.Equal
            (
                4,
                _results?.Count
            );
        }
    }
}