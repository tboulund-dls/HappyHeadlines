using StackExchange.Redis;

namespace infrastrucure.Context;

public class RedisDbContext
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _db;
    
    public RedisDbContext(string connectionString)
    {
        _redis = ConnectionMultiplexer.Connect(connectionString);
        _db = _redis.GetDatabase();
    }
    
    public IDatabase GetDatabase()
    {
        return _db;
    }
}