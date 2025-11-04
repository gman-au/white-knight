using System.Reflection;
using System.Text.Json.Serialization;

namespace White.Knight.Abstractions.Extensions
{
    public static class MemberInfoEx
    {
        public static string GetMemberPropertyOrJsonAlias(this MemberInfo memberInfo, bool lookForAlias = true)
        {
            var result = memberInfo.Name;
            if (lookForAlias)
                result =
                    memberInfo
                        .GetCustomAttribute<JsonPropertyNameAttribute>()?
                        .Name ??
                    result;

            return result;
        }
    }
}