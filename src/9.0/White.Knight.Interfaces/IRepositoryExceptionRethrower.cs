using System;

namespace White.Knight.Interfaces
{
    public interface IRepositoryExceptionRethrower
    {
        Exception Rethrow(Exception exception);
    }
}