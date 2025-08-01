using System;
using System.Linq.Expressions;

namespace White.Knight.Domain.Options
{
    public class ProjectionOptions<TD, TP>
    {
        public Expression<Func<TD, TP>> Projection { get; set; }
    }
}