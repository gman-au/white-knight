using System.Collections.Generic;

namespace White.Knight.Definition
{
	public class RepositoryResult<T>
	{
		public IEnumerable<T> Records { get; set; }

		public long Count { get; set; }
	}
}