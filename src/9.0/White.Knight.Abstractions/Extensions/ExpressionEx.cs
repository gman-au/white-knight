using System.Linq.Expressions;

namespace White.Knight.Abstractions.Extensions
{
	internal static class ExpressionEx
	{
		internal class ReplaceExpressionVisitor(Expression oldValue, Expression newValue) : ExpressionVisitor
		{
			public override Expression Visit(Expression node)
			{
				return node == oldValue ? newValue : base.Visit(node);
			}
		}
	}
}