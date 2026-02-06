using System.Text.Json;
using StackExchange.Redis;
using TodoManagement.Application.Abstractions.Caching;
using Microsoft.Extensions.Logging;

namespace TodoManagement.Infrastructure.Caching;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase? _database;
    private readonly ILogger<RedisCacheService> _logger;

    public RedisCacheService(
        IConnectionMultiplexer redis,
        ILogger<RedisCacheService> logger)
    {
        _logger = logger;

        try
        {
            _database = redis.GetDatabase();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Redis not available during startup");
            _database = null;
        }
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        if (_database is null)
            return default;

        try
        {
            var value = await _database.StringGetAsync(key);

            if (value.IsNullOrEmpty)
                return default;

            return JsonSerializer.Deserialize<T>(value!);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Redis GET failed for key {Key}", key);
            return default;
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan ttl)
    {
        if (_database is null)
            return;

        try
        {
            await _database.StringSetAsync(
                key,
                JsonSerializer.Serialize(value),
                ttl
            );
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Redis SET failed for key {Key}", key);
        }
    }

    public async Task RemoveAsync(string key)
    {
        if (_database is null)
            return;

        try
        {
            await _database.KeyDeleteAsync(key);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Redis DELETE failed for key {Key}", key);
        }
    }
}
