using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using White.Knight.Csv.Injection;
using White.Knight.Tests.Csv.Unit.Repository;
using White.Knights.Tests.Integration;
using TestContextBase = White.Knights.Tests.Integration.Context.TestContextBase;

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
        public async Task Test_Search_All_Users()
        {
            await
                _context
                    .ArrangeTableDataAsync();

            await
                _context
                    .ActSearchByAll();

            _context
                .AssertRecordCountFour();
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
        }
    }
}