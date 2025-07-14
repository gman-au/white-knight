using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using White.Knight.Abstractions.Definition;

namespace White.Knight.Abstractions.Extensions
{
    internal static class BinaryExpressionEx
    {
        public static ISubQuery ParseBinary(this BinaryExpression b, ref RootQuery rootQuery)
        {
            IEnumerable<ExpressionType> applicableLogicalExpressionTypes =
            [
                ExpressionType.AndAlso,
                ExpressionType.Or
            ];
            
            IEnumerable<ExpressionType> applicableOperatorExpressionTypes =
            [
                ExpressionType.Equal,
                ExpressionType.NotEqual,
                ExpressionType.LessThan,
                ExpressionType.LessThanOrEqual,
                ExpressionType.GreaterThan,
                ExpressionType.GreaterThanOrEqual
            ];

            var found = false;

            if (applicableLogicalExpressionTypes.Contains(b.NodeType))
            {
                found = true;
                
                var result = new QuerySubQuery { Operator = b.NodeType.ToString() };

                var left = b.Left;
                var right = b.Right;

                switch (left)
                {
                    case BinaryExpression bLeft:
                        result.OperandLeft = ParseBinary(bLeft, ref rootQuery);
                        break;
                    case MethodCallExpression mLeft:
                        result.OperandLeft = mLeft.ParseMethodCall(ref rootQuery);
                        break;
                    case ConstantExpression cLeft:
                        result.OperandLeft = cLeft.ParseConstant();
                        break;
                    default:
                        return null;
                }

                switch (right)
                {
                    case BinaryExpression bRight:
                        result.OperandRight = ParseBinary(bRight, ref rootQuery);
                        break;
                    case MethodCallExpression mRight:
                        result.OperandRight = mRight.ParseMethodCall(ref rootQuery);
                        break;
                    case ConstantExpression cRight:
                        result.OperandRight = cRight.ParseConstant();
                        break;
                    default:
                        return null;
                }
                
                return result;
            }

            if (applicableOperatorExpressionTypes.Contains(b.NodeType))
            {
                found = true;
                
                var result =
                    ParseSubQuery(
                        b.Left,
                        b.Right,
                        b.NodeType
                    );

                return result;
            }

            if (!found)
                throw new System.Exception($"Unhandled expression node type [{b.NodeType}]");
            
            return null;
        }

        private static StringSubQuery ParseSubQuery(Expression left, Expression right, ExpressionType expressionType)
        {
            var result = new StringSubQuery { Operator = expressionType.ToString() };

            if (left is not MemberExpression memberExpression)
                return null;

            result.OperandLeft = memberExpression.ExtractMember();

            if (right is not MemberExpression fieldExpression)
                return null;

            result.OperandRight = fieldExpression.ExtractValue();
            result.OperandType = fieldExpression.Type;

            if (result.OperandLeft == null || result.OperandRight == null)
                return null;

            return result;
        }
    }
}