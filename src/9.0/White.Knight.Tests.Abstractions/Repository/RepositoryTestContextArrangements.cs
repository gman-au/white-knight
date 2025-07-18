﻿using System;
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
        private IServiceProvider _serviceProvider;
        protected IConfigurationRoot Configuration;
        protected ServiceCollection ServiceCollection;

        public async Task ArrangeRepositoryDataAsync()
        {
            var testHarness =
                _serviceProvider
                    .GetRequiredService<ITestHarness>();

            _abstractedRepositoryTestData =
                await
                    testHarness
                        .GenerateRepositoryTestDataAsync();
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
            _serviceProvider =
                ServiceCollection
                    .BuildServiceProvider();

            _sut =
                _serviceProvider
                    .GetRequiredService<IRepository<Customer>>();
        }
    }
}