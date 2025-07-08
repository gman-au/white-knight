using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using White.Knight.Interfaces;

namespace White.Knight.Csv
{
    public class CsvRepositoryOptions<T>(
        ICsvLoader<T> csvLoader,
        IRepositoryExceptionWrapper exceptionWrapper,
        ILogger<CsvRepositoryOptions<T>> logger = null)
        : IRepositoryOptions<T>
    {
        public ICsvLoader<T> CsvLoader { get; set; } = csvLoader;

        public IRepositoryExceptionWrapper ExceptionWrapper { get; set; } = exceptionWrapper;

        public ILogger<CsvRepositoryOptions<T>> Logger { get; set; } = logger ?? NullLogger<CsvRepositoryOptions<T>>.Instance;
    }
}