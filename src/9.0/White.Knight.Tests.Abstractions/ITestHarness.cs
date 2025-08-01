using System.Threading.Tasks;
using White.Knight.Tests.Abstractions.Data;

namespace White.Knight.Tests.Abstractions
{
    public interface ITestHarness
    {
        public Task<AbstractedRepositoryTestData> SetupRepositoryTestDataAsync();
    }
}