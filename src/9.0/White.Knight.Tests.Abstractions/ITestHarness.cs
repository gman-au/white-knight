using System;
using System.Threading.Tasks;
using White.Knight.Tests.Abstractions.Data;

namespace White.Knight.Tests.Abstractions
{
    public interface ITestHarness : IDisposable
    {
        public Task<AbstractedRepositoryTestData> GenerateRepositoryTestDataAsync();
    }
}