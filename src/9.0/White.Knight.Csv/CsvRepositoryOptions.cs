using White.Knight.Interfaces;

namespace White.Knight.Csv
{
    public class CsvRepositoryOptions<T>(
        ICsvLoader<T> csvLoader,
        IRepositoryExceptionWrapper exceptionWrapper)
        : IRepositoryOptions<T>
    {
        public ICsvLoader<T> CsvLoader { get; set; } = csvLoader;

        public IRepositoryExceptionWrapper ExceptionWrapper { get; set; } = exceptionWrapper;
    }
}