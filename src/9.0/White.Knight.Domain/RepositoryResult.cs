using System.Collections.Generic;

namespace White.Knight.Domain
{
	public class RepositoryResult<T>
	{
		public IEnumerable<T> Records { get; set; }

		public long Count { get; set; }
	}
}