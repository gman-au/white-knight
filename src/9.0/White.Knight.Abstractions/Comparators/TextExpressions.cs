using System;
using System.Linq.Expressions;

namespace White.Knight.Abstractions.Comparators
{
    public static class TextExpressions
    {
        internal static Expression<Func<string, bool>> TextStartsWith(string value) => o => o.StartsWith(value);

        internal static Expression<Func<string, bool>> TextEndsWith(string value) => o => o.EndsWith(value);

        internal static Expression<Func<string, bool>> TextContains(string value) => o => o.Contains(value);

        internal static Expression<Func<string, bool>> TextExactMatch(string value) => o => o.Equals(value);
    }
}