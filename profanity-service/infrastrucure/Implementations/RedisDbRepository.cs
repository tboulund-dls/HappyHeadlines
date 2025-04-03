using System.Text.Json;
using infrastrucure.Context;
using infrastrucure.interfaces;
using infrastrucure.Models;
using service.Models;
using StackExchange.Redis;

namespace infrastrucure.implementations;

public class RedisDbRepository : ICacheRepository
{
    private readonly RedisDbContext _dbContext;
    private readonly TimeSpan _expiry;
    
    public RedisDbRepository(RedisDbContext dbContext, TimeSpan expiry)
    {
        _dbContext = dbContext;
        _expiry = expiry;
    }
    
    public async Task<WordModel?> GetByIdAsync(WordModel word)
    {
        var wordResult = await _dbContext.GetDatabase().StringGetAsync(word.ToString());
        return wordResult.HasValue ? JsonSerializer.Deserialize<WordModel>(wordResult) : null;
    }
    
    public async Task<WordModel> CreateOrUpdateAsync(WordModel word)
    {
        var serializedValue = JsonSerializer.Serialize(word);
        await _dbContext.GetDatabase().StringSetAsync(word.Word, serializedValue, _expiry);
        return word;
    }
    
    public async Task<bool> DeleteAsync(WordModel word)
    {
        return await _dbContext.GetDatabase().KeyDeleteAsync(word.Word);
    }
}