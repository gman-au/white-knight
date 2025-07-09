using System;
using System.Linq;
using System.Linq.Expressions;
using White.Knight.Abstractions.Command;
using White.Knight.Interfaces.Command;

namespace White.Knight.Abstractions.Fluent
{
	public static class FluentSingleEx
	{
		public static ISingleRecordCommand<TD> ToSingleRecordCommand<TD>(this object key) =>
			new SingleRecordCommand<TD>
			{
				Key = key,
			};

        public static ISingleRecordCommand<T> WithStrategy<T>(
            this ISingleRecordCommand<T> command,
            Expression<Func<IQueryable<T>, IQueryable<T>>> navigation
        )
            where T : new() =>
            command
                .WithStrategy<T, ISingleRecordCommand<T>>(navigation);
	}
}