using StackExchange.Redis;
using WeChooz.TechAssessment.Application.Interfaces;

namespace WeChooz.TechAssessment.Shared;

public class RedisLockService(IConnectionMultiplexer connectionMultiplexer) : IRedisLockService
{
    private TimeSpan DefaultRetryDelay => TimeSpan.FromMinutes(1);
    private readonly IDatabase _redisDatabase = connectionMultiplexer.GetDatabase();

    /// <summary>
    /// Acquires a distributed lock.
    /// </summary>
    /// <param name="lockKey">The unique key for the lock.</param>
    /// <param name="lockValue">A unique identifier for the lock holder (e.g., GUID).</param>
    /// <param name="expiration">The expiration time for the lock, how much time the object will be locked</param>
    /// <returns>True if the lock was acquired, false otherwise.</returns>
    public async Task<bool> AcquireLockAsync(string lockKey, string lockValue, TimeSpan expiration)
    {
        // Use SET command with NX (only set if not exists) and PX (set expiry in milliseconds)
        return await _redisDatabase.StringSetAsync(lockKey, lockValue, expiration, When.NotExists);
    }

    public async Task<bool> AcquireLockAsync(string lockKey, string lockValue, TimeSpan expiration, int maxRetries, TimeSpan retryDelay = default)
    {
        if (retryDelay == default)
        {
            retryDelay = DefaultRetryDelay;
        }

        var retryCount = 0;

        while (retryCount < maxRetries)
        {
            if (await AcquireLockAsync(lockKey, lockValue, expiration))
            {
                return true;
            }

            retryCount++;

            await Task.Delay(retryDelay);
        }

        return false;
    }

    /// <summary>
    /// Releases a distributed lock.
    /// </summary>
    /// <param name="lockKey">The unique key for the lock.</param>
    /// <param name="lockValue">The unique identifier for the lock holder.</param>
    /// <returns>True if the lock was released, false otherwise.</returns>
    public async Task<bool> ReleaseLockAsync(string lockKey, string lockValue)
    {
        // Lua script to release the lock only if the lockValue matches
        const string luaScript = @"
            if redis.call('GET', KEYS[1]) == ARGV[1] then
                return redis.call('DEL', KEYS[1])
            else
                return 0
            end";

        var result = await _redisDatabase.ScriptEvaluateAsync(
            luaScript,
            new RedisKey[] { lockKey },
            new RedisValue[] { lockValue });

        return (int)result == 1;
    }
}