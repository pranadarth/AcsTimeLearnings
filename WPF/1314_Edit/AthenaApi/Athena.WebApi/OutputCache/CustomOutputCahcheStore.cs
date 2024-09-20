using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using System.Web.Caching;

namespace Athena.WebApi.OutputCache
{
    //public class CustomOutputCahcheStore : IOutputCacheStore
    //{
    //    private class CacheEntry
    //    {
    //        public byte[] Value { get; set; }
    //        public string[] Tags { get; set; }
    //        public DateTime ExpirationTime { get; set; }
    //    }

    //    private readonly ConcurrentDictionary<string, CacheEntry> _cache = new ConcurrentDictionary<string, CacheEntry>();

    //    public ValueTask EvictByTagAsync(string tag, CancellationToken cancellationToken)
    //    {
    //        foreach (var key in _cache.Keys)
    //        {
    //            if (_cache[key].Tags.Contains(tag))
    //            {
    //                _cache.TryRemove(key, out _);
    //            }
    //        }
    //        return ValueTask.CompletedTask;
    //    }

    //    public ValueTask<byte[]?> GetAsync(string key, CancellationToken cancellationToken)
    //    {
    //        _cache.TryGetValue(key, out var entry);
    //        if (entry != null && entry.ExpirationTime > DateTime.UtcNow)
    //        {
    //            return new ValueTask<byte[]?>(entry.Value);
    //        }
    //        return new ValueTask<byte[]?>();
    //    }

    //    public ValueTask SetAsync(string key, byte[] value, string[]? tags, TimeSpan validFor, CancellationToken cancellationToken)
    //    {
    //        var entry = new CacheEntry
    //        {
    //            Value = value,
    //            Tags = tags ?? Array.Empty<string>(),
    //            ExpirationTime = DateTime.UtcNow.Add(validFor)
    //        };

    //        _cache[key] = entry;
    //        return ValueTask.CompletedTask;
    //    }
    //}
}
