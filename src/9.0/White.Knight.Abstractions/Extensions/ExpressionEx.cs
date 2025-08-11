using System.Linq.Expressions;

namespace White.Knight.Abstractions.Extensions
{
    public static class ExpressionEx
    {
        public static string GetPropertyExpressionPath(
            this Expression ex,
            ref string name,
            string separator = ".",
            bool lookForAlias = true)
        {
            if (ex is LambdaExpression lambdaExpression)
                return
                    lambdaExpression
                        .Body
                        .GetPropertyExpressionPath(ref name, separator, lookForAlias);

            if (ex is MemberExpression memberExpression)
            {
                if (memberExpression.Expression is MemberExpression subMemberExpression)
                    subMemberExpression
                        .GetPropertyExpressionPath(ref name, separator, lookForAlias);

                if (!string.IsNullOrEmpty(name))
                    name += separator;

                name +=
                    memberExpression
                        .Member
                        .GetMemberPropertyOrJsonAlias(lookForAlias);
            }

            if (ex is ConditionalExpression conditionalExpression)
                return
                    conditionalExpression
                        .IfFalse
                        .GetPropertyExpressionPath(ref name, separator, lookForAlias);

            if (ex is UnaryExpression unaryExpression)
                if (unaryExpression.NodeType == ExpressionType.Convert)
                    if (unaryExpression.Operand is MemberExpression convertMemberExpression)
                    {
                        return
                            convertMemberExpression
                                .GetPropertyExpressionPath(ref name, separator, lookForAlias);
                    }

            return name;
        }
    }
}