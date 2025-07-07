using System;

namespace White.Knight.Interfaces
{
	public interface IRepositoryException
	{
		bool IsApplicable(Exception exception);
	}
}