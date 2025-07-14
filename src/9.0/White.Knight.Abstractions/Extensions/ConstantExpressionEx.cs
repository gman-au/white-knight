using System.Linq.Expressions;
using White.Knight.Abstractions.Definition;

namespace White.Knight.Abstractions.Extensions
{
    internal static class ConstantExpressionEx
    {
        public static ISubQuery ParseConstant(this ConstantExpression constantExpression)
        {
            var result = new StringSubQuery
            {
                Operator = constantExpression.NodeType.ToString(),
                OperandLeft = null,
                OperandRight = constantExpression.Value,
                OperandType = constantExpression.Type
            };

            return result;
        }
    }
}