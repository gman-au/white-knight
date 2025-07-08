using System;
using System.Linq.Expressions;

namespace White.Knight.Abstractions.Comparators
{
    public static class EqualityExpressions
    {
        public static Expression<Func<int, bool>> Match(int value) => o => o == value;
        public static Expression<Func<int?, bool>> Match(int? value) => o => o == value;
        public static Expression<Func<bool, bool>> Match(bool value) => o => o == value;
        public static Expression<Func<bool?, bool>> Match(bool? value) => o => o == value;
        public static Expression<Func<DateTime, bool>> Match(DateTime value) => o => o == value;
        public static Expression<Func<DateTime?, bool>> Match(DateTime? value) => o => o == value;
        public static Expression<Func<Guid, bool>> Match(Guid value) => o => o == value;
        public static Expression<Func<Guid?, bool>> Match(Guid? value) => o => o == value;
        public static Expression<Func<string, bool>> Match(string value) => o => o == value;
    }
}