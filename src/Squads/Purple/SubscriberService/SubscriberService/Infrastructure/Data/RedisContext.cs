using StackExchange.Redis;

namespace SubscriberService.Infrastructure.Data;

public class RedisContext(string connectionString)
{
    private readonly IConnectionMultiplexer _redis = ConnectionMultiplexer.Connect(connectionString);
    
    public IDatabase Database => _redis.GetDatabase();
}