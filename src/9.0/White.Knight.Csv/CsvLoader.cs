using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.Extensions.Options;
using White.Knight.Csv.Options;
using White.Knight.Definition.Exceptions;

namespace White.Knight.Csv
{
    public class CsvLoader<TD>(IOptions<CsvRepositoryConfigurationOptions> optionsAccessor) : ICsvLoader<TD>
    {
        private readonly CsvRepositoryConfigurationOptions _configurationOptions = optionsAccessor?.Value;

        public async Task<IQueryable<TD>> LoadAsync(CancellationToken cancellationToken)
        {
            var fileName =
                $"{typeof(TD).Name}.csv"
                    .ToLowerInvariant();

            var folderPath =
                _configurationOptions
                    .FolderPath ??
                throw new MissingConfigurationException("CsvRepositoryOptions -> FolderPath");

            var filePath =
                Path
                    .Combine(folderPath, fileName);

            using var reader = new StreamReader(filePath);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records =
                    csvReader
                        .GetRecords<TD>();

            return
                records
                    .AsQueryable();
        }
    }
}