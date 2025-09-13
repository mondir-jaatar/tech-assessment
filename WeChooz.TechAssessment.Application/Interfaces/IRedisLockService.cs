namespace WeChooz.TechAssessment.Application.Interfaces;

public interface IRedisLockService
{
    Task<bool> AcquireLockAsync(string lockKey, string lockValue, TimeSpan expiration);
    Task<bool> AcquireLockAsync(string lockKey, string lockValue, TimeSpan expiration, int maxRetries, TimeSpan retryDelay = default);
    Task<bool> ReleaseLockAsync(string lockKey, string lockValue);
}