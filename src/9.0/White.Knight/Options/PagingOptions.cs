using System;
using System.Linq.Expressions;

namespace White.Knight.Options
{
	public class PagingOptions<T>
	{
		public int? Page { get; set; }

		public int? PageSize { get; set; }

		public Expression<Func<T, object>> OrderBy { get; set; }

		public bool? Descending { get; set; }
	}
}