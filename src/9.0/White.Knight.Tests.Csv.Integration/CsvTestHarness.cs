using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.Extensions.Options;
using White.Knight.Csv.Options;
using White.Knights.Tests.Integration;
using White.Knights.Tests.Integration.Data;

namespace White.Knight.Tests.Csv.Integration
{
    public class CsvTestHarness(
        ITestDataGenerator testDataGenerator,
        IOptions<CsvRepositoryConfigurationOptions> optionsAccessor)
        : ITestHarness
    {
        private readonly CsvRepositoryConfigurationOptions _options = optionsAccessor.Value;

        public async Task LoadTableDataAsync()
        {
            var testData =
                testDataGenerator
                    .BuildRepositoryTestData();

            // put 'records' into tables i.e. write to CSV files in advance of the tests
            await WriteRecordsAsync(testData.Addresses);
            await WriteRecordsAsync(testData.Customers);
            await WriteRecordsAsync(testData.Orders);
        }

        private async Task WriteRecordsAsync<T>(IEnumerable<T> records)
        {
            var fileName =
                $"{typeof(T).Name}.csv"
                    .ToLowerInvariant();

            var filePath =
                Path
                    .Combine(_options.FolderPath, fileName);

            await using var writer = new StreamWriter(filePath);
            await using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

            await
                csvWriter.WriteRecordsAsync(records);
        }
    }
}