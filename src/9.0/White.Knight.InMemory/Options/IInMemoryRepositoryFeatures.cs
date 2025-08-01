using White.Knight.Interfaces;

namespace White.Knight.InMemory.Options
{
    public interface IInMemoryRepositoryFeatures<T> : IRepositoryFeatures
    {
        public ICache<T> Cache { get; set; }
    }
}