using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using White.Knight.InMemory.Options;
using White.Knight.Tests.Abstractions;
using White.Knight.Tests.Abstractions.Data;
using White.Knight.Tests.Domain;

namespace White.Knight.InMemory.Tests.Integration
{
    public class InMemoryTestHarness(
        ICache<Address> addressCache,
        ICache<Customer> customerCache,
        ICache<Order> orderCache,
        ITestDataGenerator testDataGenerator,
        IOptions<InMemoryRepositoryConfigurationOptions> optionsAccessor)
        : ITestHarness
    {
        private readonly InMemoryRepositoryConfigurationOptions _options = optionsAccessor.Value;

        public async Task<AbstractedRepositoryTestData> SetupRepositoryTestDataAsync()
        {
            var testData =
                testDataGenerator
                    .GenerateRepositoryTestData();

            // put 'records' into tables i.e. write to repository stores *in advance* of the tests
            await WriteRecordsAsync(addressCache, testData.Addresses, o => o.AddressId);
            await WriteRecordsAsync(customerCache, testData.Customers, o => o.CustomerId);
            await WriteRecordsAsync(orderCache, testData.Orders, o => o.OrderId);

            return testData;
        }

        private async Task WriteRecordsAsync<T>(
            ICache<T> cache,
            IEnumerable<T> records,
            Expression<Func<T, object>> keySelector)
        {
            foreach (var record in records)
            {
                var keyValue =
                    keySelector
                        .Compile()
                        .Invoke(record);

                await
                    cache
                        .WriteAsync(
                            keyValue,
                            record,
                            CancellationToken.None
                        );
            }
        }

        public void Dispose()
        {
        }
    }
}