using System;
using System.Linq.Expressions;

namespace White.Knight.Definition.Options
{
	public class ProjectionOptions<TD, TP>
	{
		public Expression<Func<TD, TP>> Projection { get; set; }
	}
}