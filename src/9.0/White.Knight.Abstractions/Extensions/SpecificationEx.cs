using System;
using System.Linq;
using System.Linq.Expressions;
using White.Knight.Abstractions.Definition;
using White.Knight.Abstractions.Specifications;
using White.Knight.Domain.Exceptions;
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

		public static RootQuery ToRootQuery<T>(this ISpecification<T> specification)
		{
			var result = new RootQuery
			{
				Query = null,
				Joins = null
			};

			var expression =
				specification
					.ToExpression();

			if (expression is LambdaExpression)
			{
				expression
					.BuildRootQuery(ref result);

				result = AddAliases(result);

				return result;
			}

			throw new UnparsableSpecificationException();
		}

		public static RootQuery ToRootQuery(this LambdaExpression expression)
		{
			var result = new RootQuery
			{
				Query = null,
				Joins = null
			};

			expression
				.BuildRootQuery(ref result);

			if (result.Query == null)
				throw new UnparsableSpecificationException();

			return result;
		}

		public static void BuildRootQuery(this LambdaExpression expression, ref RootQuery rootQuery)
		{
			ISubQuery currentSubQuery = expression.Body switch
			{
				MethodCallExpression methodCallExpression => methodCallExpression.ParseMethodCall(ref rootQuery),
				BinaryExpression binaryExpression => binaryExpression.ParseBinary(ref rootQuery),
				ConstantExpression constantExpression => constantExpression.ParseConstant(),
				_ => null
			};

			rootQuery.Query = currentSubQuery;
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

		private static RootQuery AddAliases(RootQuery rootQuery)
		{
			var currentAlias = 'a';

			rootQuery.Alias =
				currentAlias
					.ToString();

			foreach (var join in rootQuery?.Joins ?? Enumerable.Empty<SelfJoin>())
			{
				currentAlias++;
				if (currentAlias > 'z')
					throw new ExcessiveJoinsException();

				join.Alias =
					currentAlias
						.ToString();
			}

			return rootQuery;
		}
	}
}