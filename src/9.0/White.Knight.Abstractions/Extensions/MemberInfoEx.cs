using System.Reflection;
using System.Text.Json.Serialization;

namespace White.Knight.Abstractions.Extensions
{
    public static class MemberInfoEx
    {
        public static string GetMemberPropertyOrJsonAlias(this MemberInfo memberInfo)
        {
            return
                memberInfo
                    .GetCustomAttribute<JsonPropertyNameAttribute>()?
                    .Name ??
                memberInfo
                    .Name;
        }
    }
}