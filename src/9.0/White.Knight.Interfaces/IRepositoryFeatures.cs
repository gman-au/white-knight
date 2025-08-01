using Microsoft.Extensions.Logging;

namespace White.Knight.Interfaces
{
    public interface IRepositoryFeatures
    {
        public IRepositoryExceptionRethrower ExceptionRethrower { get; set; }

        public ILoggerFactory LoggerFactory { get; set; }
    }
}