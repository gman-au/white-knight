using System.Linq.Expressions;

namespace White.Knight.Abstractions.Extensions
{
    public static class ExpressionEx
    {
        public static string GetPropertyExpressionPath(
            ref string name,
            Expression ex,
            string separator = ".")
        {
            if (ex is MemberExpression memberExpression)
            {
                if (memberExpression.Expression is MemberExpression subMemberExpression)
                {
                    GetPropertyExpressionPath(ref name, subMemberExpression);
                }

                if (!string.IsNullOrEmpty(name))
                    name += separator;

                name +=
                    memberExpression
                        .Member
                        .GetMemberPropertyOrJsonAlias();
            }
            if (ex is ConditionalExpression conditionalExpression)
            {
                return GetPropertyExpressionPath(ref name, conditionalExpression.IfFalse);
            }

            return name;
        }
    }
}