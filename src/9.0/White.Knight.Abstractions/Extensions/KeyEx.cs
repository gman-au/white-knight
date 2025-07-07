using System;
using System.Linq.Expressions;
using White.Knight.Definition.Exceptions;

namespace White.Knight.Abstractions.Extensions
{
    public static class KeyEx
    {
        private static Expression<Func<TD, bool>> BuildKeySelectorExpression<TD>(
            this Expression<Func<TD, object>> keyExpression,
            object key)
        {
            var param =
                Expression
                    .Parameter(
                        typeof(TD),
                        "o"
                    );

            var body =
                keyExpression
                    .Body;

            GetTypeFromExpression<TD>(
                body,
                out var memberName,
                out var type
            );

            if (type != key.GetType())
                throw new UnrecognisedKeyException<TD>(key.GetType());

            var expression = Expression.Lambda<Func<TD, bool>>(
                Expression.Equal(
                    Expression.Property(
                        param,
                        memberName
                    ),
                    Expression.Constant(
                        key,
                        type
                    )
                ), param
            );

            return expression;
        }

        private static void GetTypeFromExpression<TD>(Expression expression, out string memberName, out Type type)
        {
            memberName = string.Empty;
            type = typeof(object);

            if (expression is MemberExpression member)
            {
                memberName = member.Member.Name;
                type = expression.Type;
            }
            else if (expression is UnaryExpression unary)
            {
                if (unary.Operand is MemberExpression unaryMember)
                {
                    memberName = unaryMember.Member.Name;
                    type = unaryMember.Type;
                }
            }

            if (type == typeof(object))
                throw new MisconfiguredSelectorException(typeof(TD));
        }
    }
}