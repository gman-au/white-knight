using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace White.Knight.Abstractions.Extensions
{
    internal static class MemberExpressionEx
    {
        public static string ExtractValue(this MemberExpression fieldExpression)
        {
            if (fieldExpression.Expression is not ConstantExpression constantExpression)
                return null;

            var field =
                constantExpression
                    .Type
                    .GetFields()
                    .FirstOrDefault(o => o.Name == "value");

            if (field == null) return null;

            var value =
                field
                    .GetValue(constantExpression.Value);

            return
                value?
                    .ToString();

        }

        public static string ExtractMember(this MemberExpression memberExpression)
        {
            var result = new StringBuilder();
            var currentExpression = memberExpression.Expression;
            while (currentExpression is MemberExpression propertyExpression)
            {
                if (result.Length > 0) 
                    result.Append('.');
                
                result
                    .Append(GetMemberPropertyOrJsonAlias(propertyExpression.Member));
                
                currentExpression = propertyExpression.Expression;
            }
            
            if (result.Length > 0) 
                result.Append('.');

            result
                .Append(GetMemberPropertyOrJsonAlias(memberExpression.Member));

            return
                result
                    .ToString();
        }

        private static string GetMemberPropertyOrJsonAlias(MemberInfo member) =>
            member
                .GetCustomAttribute<JsonPropertyNameAttribute>()?
                .Name ??
            member
                .Name;
    }
}