using infrastrucure.Context;
using infrastrucure.interfaces;
using infrastrucure.Mappers;
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
        try
        {
            var words = await _dbContext.Words.Find(_ => true).ToListAsync();
            return words.Select(x => x.ToModel());
        }
        catch (Exception e)
        {
            throw new MongoException("Error getting words");
        }
        
    }

    public async Task<WordModel> Lookup(string profanity)
    {
        try
        {
            var word = await _dbContext.Words.Find(x => x.Word == profanity).FirstOrDefaultAsync();
        
            return word?.ToModel();
        }
        catch (Exception e)
        {
            throw new MongoException("Error getting word");
        }
        
    }

    public async Task<bool> AddWord(WordModel word)
    {
        var wordEntity = word.ToDbModel();
        try
        {
            await _dbContext.Words.InsertOneAsync(wordEntity);
            return true;
        }
        catch (Exception e)
        {
            throw new MongoException("An error occured while adding word");
        }
    }

    public async Task<bool> DeleteWord(string word)
    {
        try
        {
            var result = await _dbContext.Words.DeleteOneAsync(x => x.Word == word);
            return result.DeletedCount > 0;
        }
        catch (Exception e)
        {
            throw new MongoException("An error occured while deleting word");
        }
    }
}