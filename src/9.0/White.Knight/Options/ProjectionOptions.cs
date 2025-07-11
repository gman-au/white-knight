using System;
using System.Linq.Expressions;

namespace White.Knight.Options
{
	public class ProjectionOptions<TD, TP>
	{
		public Expression<Func<TD, TP>> Projection { get; set; }
	}
}