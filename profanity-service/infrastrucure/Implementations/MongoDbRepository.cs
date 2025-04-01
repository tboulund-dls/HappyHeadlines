using infrastrucure.Context;
using infrastrucure.interfaces;
using infrastrucure.Mappers;
using infrastrucure.Models;
using MongoDB.Driver;
using service.Models;

namespace infrastrucure.implementations;

public class MongoDbRepository : IRepository
{
    
    private readonly MongoDbContext _dbContext;
    
    public MongoDbRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<WordModel>> GetWords()
    {
        var words = await _dbContext.Words.Find(_ => true).ToListAsync();
        return words.Select(x => x.ToModel());
    }

    public async Task<WordModel> Lookup(string profanity)
    {
        var word = await _dbContext.Words.Find(x => x.Word == profanity).FirstOrDefaultAsync();
        
        return word?.ToModel();
    }

    public async Task<bool> AddWord(WordModel word)
    {
        var wordEntity = word.ToDbModel();
        try
        {
            await _dbContext.Words.InsertOneAsync(wordEntity);
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteWord(string word)
    {
        var result = await _dbContext.Words.DeleteOneAsync(x => x.Word == word);
        return result.DeletedCount > 0;
    }
}