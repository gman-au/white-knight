using System.Reflection;
using System.Threading.Tasks;
using White.Knight.InMemory.Injection;
using White.Knight.InMemory.Tests.Integration.Repositories;
using White.Knight.Tests.Abstractions;
using White.Knight.Tests.Abstractions.Extensions;
using White.Knight.Tests.Abstractions.Repository;
using White.Knight.Tests.Abstractions.Tests;
using Xunit.Abstractions;

namespace White.Knight.InMemory.Tests.Integration
{
    public class InMemoryRepositoryTests(ITestOutputHelper helper)
        : AbstractedRepositoryTests(new InMemoryRepositoryTestContext(helper))
    {
        private static readonly Assembly RepositoryAssembly =
            Assembly
                .GetAssembly(typeof(AddressRepository));

        private class InMemoryRepositoryTestContext : RepositoryTestContextBase, IRepositoryTestContext
        {
            public InMemoryRepositoryTestContext(ITestOutputHelper testOutputHelper)
            {
                // specify in memory harness
                LoadTestConfiguration<InMemoryTestHarness>();

                // service initialisation
                ServiceCollection
                    .AddInMemoryRepositories(Configuration)
                    .AddAttributedInMemoryRepositories(RepositoryAssembly);

                // redirect ILogger output to Xunit console
                ServiceCollection
                    .ArrangeXunitOutputLogging(testOutputHelper);

                ServiceCollection
                    .AddInMemoryRepositoryFeatures();

                LoadServiceProvider();
            }
        }

        // Example of an overridden test
        [Fact]
        public override async Task Test_Search_Split_Page()
        {
            await
                GetContext()
                    .ArrangeRepositoryDataAsync();

            await
                GetContext()
                    .ActSearchWithPageSizeTwoAsync();

            GetContext()
                .AssertRecordCount(4);
        }
    }
}