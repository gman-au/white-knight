using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using White.Knight.Interfaces;
using White.Knight.Tests.Abstractions.Data;
using White.Knight.Tests.Domain;

namespace White.Knight.Tests.Abstractions.Repository
{
    public partial class RepositoryTestContextBase
    {
        private AbstractedRepositoryTestData _abstractedRepositoryTestData;
        protected IServiceProvider ServiceProvider;
        protected IConfigurationRoot Configuration;
        protected ServiceCollection ServiceCollection;

        public async Task ArrangeRepositoryDataAsync()
        {
            var testHarness =
                ServiceProvider
                    .GetRequiredService<ITestHarness>();

            _abstractedRepositoryTestData =
                await
                    testHarness
                        .SetupRepositoryTestDataAsync();
        }

        protected void LoadTestConfiguration<T>() where T : class, ITestHarness
        {
            ServiceCollection = [];

            ServiceCollection
                .AddTransient<ITestDataGenerator, TestDataGenerator>()
                .AddTransient<ITestHarness, T>();

            var path =
                Path
                    .GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                ?? throw new Exception("Could not get directory name");

            Configuration =
                new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("appsettings.json")
                    .Build();
        }

        protected void LoadServiceProvider()
        {
            ServiceProvider =
                ServiceCollection
                    .BuildServiceProvider();

            Sut =
                ServiceProvider
                    .GetRequiredService<IRepository<Customer>>();
        }
    }
}