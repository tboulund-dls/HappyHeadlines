using System.Text.Json;
using infrastrucure.Context;
using infrastrucure.Models;
using service.Models;
using StackExchange.Redis;

namespace infrastrucure.implementations;

public class RedisDbRepository
{
    private readonly RedisDbContext _dbContext;
    private readonly TimeSpan _expiry;
    
    public RedisDbRepository(RedisDbContext dbContext, TimeSpan expiry)
    {
        _dbContext = dbContext;
        _expiry = expiry;
    }
    
    public async Task<WordModel> CreateAsync(WordModel word)
    {
        var serializedValue = JsonSerializer.Serialize(word);
        await _dbContext.GetDatabase().StringSetAsync(word.Word, serializedValue, _expiry);
        return word;
    }
}