namespace White.Knight.Interfaces
{
    public interface IRepositoryOptions<T>
    {
        public IRepositoryExceptionWrapper ExceptionWrapper { get; set; }
    }
}