using System;
using System.Linq.Expressions;

namespace White.Knight.Abstractions.Comparators
{
    public static class TextExpressions
    {
        internal static Expression<Func<string, bool>> TextStartsWith(string value) => o => value.StartsWith(o);

        internal static Expression<Func<string, bool>> TextEndsWith(string value) => o => value.EndsWith(o);

        internal static Expression<Func<string, bool>> TextContains(string value) => o => value.Contains(o);

        internal static Expression<Func<string, bool>> TextExactMatch(string value) => o => value.Equals(o);
    }
}