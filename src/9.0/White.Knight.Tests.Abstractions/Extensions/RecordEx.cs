using System.Collections.Generic;
using White.Knight.Domain;

namespace White.Knight.Tests.Abstractions.Extensions
{
    internal static class RecordEx
    {
        public static RepositoryResult<T> ToMockResults<T>(T record) where T : new() =>
            new()
            {
                Records = new List<T> { record },
                Count = record == null ? 0 : 1
            };
    }
}