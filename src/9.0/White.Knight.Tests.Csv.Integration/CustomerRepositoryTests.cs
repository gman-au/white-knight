using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using White.Knight.Csv.Injection;
using White.Knight.Tests.Csv.Unit.Repository;
using White.Knights.Tests.Integration;

namespace White.Knight.Tests.Csv.Integration
{
    public class CustomerRepositoryTests
    {
        private static readonly Assembly RepositoryAssembly = Assembly.GetAssembly(typeof(AddressRepository));

        private readonly CsvRepositoryTestContext _context;

        public CustomerRepositoryTests()
        {
            _context = new CsvRepositoryTestContext();
        }

        [Fact]
        public async Task Test_The_Test()
        {
            await
                _context
                    .ArrangeTableDataAsync();
        }

        private class CsvRepositoryTestContext : TestContextBase
        {
            public CsvRepositoryTestContext()
            {
                // specify csv harness
                LoadTestConfiguration<CsvTestHarness>();

                // service initialisation
                ServiceCollection
                    .AddCsvRepositories(Configuration)
                    .AddAttributedCsvRepositories(RepositoryAssembly);

                ServiceCollection
                    .AddCsvRepositoryOptions();

                LoadServiceProvider();
            }

            public async Task ArrangeTableDataAsync()
            {
                var testHarness =
                    ServiceProvider
                        .GetRequiredService<ITestHarness>();

                await
                    testHarness
                        .LoadTableDataAsync();
            }
        }
    }
}