using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace White.Knight.InMemory
{
    public class Cache<T> : ICache<T>
    {
        private readonly Dictionary<string, T> _dictionary = new();

        public async Task<IQueryable<T>> ReadAsync(
            CancellationToken cancellationTokens)
        {
            var keyPrefix =
                typeof(T)
                    .Name
                    .ToUpper();

            var result =
                _dictionary
                    .Where(o => o.Key.StartsWith(keyPrefix))
                    .AsQueryable()
                    .Select(o => DeepClone(o.Value));

            return result;
        }

        public async Task WriteAsync(object key, T record, CancellationToken cancellationToken)
        {
            var keyPrefix =
                typeof(T)
                    .Name
                    .ToUpper();

            var keySuffix =
                key
                    .ToString();

            _dictionary
                .Remove($"{keyPrefix}:{keySuffix}");

            _dictionary
                .Add(
                    $"{keyPrefix}:{keySuffix}",
                    DeepClone(record)
                );
        }

        public async Task RemoveAsync(object key, CancellationToken cancellationToken)
        {
            var keyPrefix =
                typeof(T)
                    .Name
                    .ToUpper();

            var keySuffix =
                key
                    .ToString();

            _dictionary
                .Remove($"{keyPrefix}:{keySuffix}");
        }

        private static T DeepClone(T obj)
        {
            var serialized = JsonSerializer.Serialize(obj);
            var cloned = JsonSerializer.Deserialize<T>(serialized);

            return cloned;
        }
    }
}