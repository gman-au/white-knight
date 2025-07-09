using System;
using System.Collections.Generic;
using System.Linq;
using White.Knight.Interfaces;

namespace White.Knight.Abstractions.Features
{
	public class RepositoryExceptionWrapper(IEnumerable<IRepositoryException> repositoryExceptions)
		: IRepositoryExceptionWrapper
	{
		public Exception Rethrow(Exception exception)
		{
			var applicableException =
				repositoryExceptions
					.FirstOrDefault(e => e.IsApplicable(exception));

			if (applicableException == null) return exception;

			if (Activator.CreateInstance(applicableException.GetType()) is Exception wrappedException)
				return wrappedException;

			return exception;
		}
	}
}