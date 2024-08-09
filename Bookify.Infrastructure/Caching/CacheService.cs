using Bookify.Application.Abstractions.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Caching
{
    internal sealed class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                var bytes = await _distributedCache.GetAsync(key, cancellationToken);

                return bytes is null ? default : Deserialize<T>(bytes);
            }
            catch (Exception)
            {

                return default;
            }

        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            return _distributedCache.RemoveAsync(key, cancellationToken);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            var bytes = Serialize(value);

            try
            {
                return _distributedCache.SetAsync(key, bytes, CacheOptions.Create(expiration), cancellationToken);
            }
            catch (Exception)
            {

                return default;
            }

        }

        private static T Deserialize<T>(byte[] bytes) => JsonSerializer.Deserialize<T>(bytes);

        private static byte[] Serialize<T>(T value)
        {
            var buffer = new ArrayBufferWriter<byte>();

            using var writer = new Utf8JsonWriter(buffer);

            JsonSerializer.Serialize(writer, value);

            return buffer.WrittenSpan.ToArray();
        }
    }
}
