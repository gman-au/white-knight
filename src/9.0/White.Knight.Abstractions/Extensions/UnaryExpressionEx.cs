using System.Linq.Expressions;

namespace White.Knight.Abstractions.Extensions
{
    public static class UnaryExpressionEx
    {
        public static string ExtractValue(this UnaryExpression fieldExpression)
        {
            if (fieldExpression.NodeType != ExpressionType.Convert) return null;

            if (fieldExpression.Operand is MemberExpression memberExpression)
            {
                return memberExpression.ExtractMember();
            }

            return null;
        }
    }
}