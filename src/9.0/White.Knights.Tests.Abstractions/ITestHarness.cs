using System.Threading.Tasks;
using White.Knights.Tests.Abstractions.Data;

namespace White.Knights.Tests.Abstractions
{
    public interface ITestHarness
    {
        public Task<AbstractedRepositoryTestData> GenerateRepositoryTestDataAsync();
    }
}