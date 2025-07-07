using System;

namespace White.Knight.Interfaces
{
	public interface IRepositoryExceptionWrapper
	{
		Exception Rethrow(Exception exception);
	}
}