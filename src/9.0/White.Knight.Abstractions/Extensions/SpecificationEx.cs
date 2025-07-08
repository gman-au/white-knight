using System;
using System.Linq.Expressions;
using White.Knight.Abstractions.Specifications;
using White.Knight.Interfaces.Spec;

namespace White.Knight.Abstractions.Extensions
{
	public static class SpecificationEx
	{
		public static ISpecification<T> Or<T>(
			this ISpecification<T> specification,
			ISpecification<T> withSpecification
		)
		{
			return new SpecificationAmalgam<T>(
				JoinExpressions(
					specification,
					withSpecification,
					Expression.Or
				)
			);
		}

		public static ISpecification<T> And<T>(
			this ISpecification<T> specification,
			ISpecification<T> withSpecification
		)
		{
			return new SpecificationAmalgam<T>(
				JoinExpressions(
					specification,
					withSpecification,
					Expression.AndAlso
				)
			);
		}

		private static Expression<Func<T, bool>> JoinExpressions<T>(
			ISpecification<T> specification,
			ISpecification<T> withSpecification,
			Func<Expression, Expression, BinaryExpression> joinFunc
		)
		{
			var parameter = Expression.Parameter(typeof(T));

			var leftExpression = specification.ToExpression();
			var rightExpression = withSpecification.ToExpression();

			var leftVisitor = new ExpressionEx.ReplaceExpressionVisitor(
				leftExpression.Parameters[0],
				parameter
			);
			var left = leftVisitor.Visit(leftExpression.Body);

			var rightVisitor = new ExpressionEx.ReplaceExpressionVisitor(
				rightExpression.Parameters[0],
				parameter
			);
			var right = rightVisitor.Visit(rightExpression.Body);

			return Expression.Lambda<Func<T, bool>>(
				joinFunc(
					left,
					right
				), parameter
			);
		}
	}
}